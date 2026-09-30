using UnityEngine;
using Aetherium.Core.Constants;
using Aetherium.Core.StateMachine;
using Aetherium.Player.Controller;
using Aetherium.Combat.Data;

namespace Aetherium.Player.States
{
    public class PlayerAttackState : PlayerBaseState
    {
        private readonly int comboIndex;
        private float stateTimer;
        private bool hasQueuedNextAttack;
        private bool hitboxActive;

        public PlayerAttackState(StateMachine stateMachine, PlayerController player, int comboIndex = 0)
            : base(stateMachine, player)
        {
            this.comboIndex = comboIndex;
        }

        public override void Enter()
        {
            stateTimer = 0f;
            hasQueuedNextAttack = false;
            hitboxActive = false;
            SubscribeInput();
            PlayAttackAnimation();
        }

        private void PlayAttackAnimation()
        {
            if (player.Animator == null) return;
            player.Animator.SetInteger(AnimationConstants.ComboIndex, comboIndex);
            player.Animator.SetTrigger(AnimationConstants.LightAttack);
        }

        public override void Tick(float deltaTime)
        {
            stateTimer += deltaTime;
            HandleHitboxWindow();
            HandleComboTransitions();
        }

        private void HandleHitboxWindow()
        {
            if (stateTimer >= 0.1f && stateTimer <= 0.35f && !hitboxActive)
            {
                hitboxActive = true;
                player.WeaponHitbox?.EnableHitbox(null);
            }
            else if (stateTimer > 0.35f && hitboxActive)
            {
                hitboxActive = false;
                player.WeaponHitbox?.DisableHitbox();
            }
        }

        private void HandleComboTransitions()
        {
            if (stateTimer >= 0.3f && hasQueuedNextAttack && comboIndex < 2)
            {
                stateMachine.ChangeState(new PlayerAttackState(stateMachine, player, comboIndex + 1));
                return;
            }

            if (stateTimer >= 0.55f)
            {
                stateMachine.ChangeState(new PlayerLocomotionState(stateMachine, player));
            }
        }

        private void SubscribeInput()
        {
            if (player.Input == null) return;
            player.Input.AttackEvent += QueueNextAttack;
            player.Input.DodgeEvent += OnDodgeTriggered;
        }

        private void UnsubscribeInput()
        {
            if (player.Input == null) return;
            player.Input.AttackEvent -= QueueNextAttack;
            player.Input.DodgeEvent -= OnDodgeTriggered;
        }

        private void QueueNextAttack() => hasQueuedNextAttack = true;

        private void OnDodgeTriggered()
        {
            float dodgeCost = player.Stats != null ? player.Stats.DodgeStaminaCost : 20f;
            if (stateTimer >= 0.25f && player.ConsumeStamina(dodgeCost))
            {
                stateMachine.ChangeState(new PlayerDodgeState(stateMachine, player));
            }
        }

        public override void Exit()
        {
            UnsubscribeInput();
            if (hitboxActive)
            {
                player.WeaponHitbox?.DisableHitbox();
            }
        }
    }
}
