using UnityEngine;
using Aetherium.Core.Constants;
using Aetherium.Core.Events;
using Aetherium.Core.StateMachine;
using Aetherium.Player.Controller;
using Aetherium.Combat.Data;
using Aetherium.Combat.Interfaces;

namespace Aetherium.Player.States
{
    public class PlayerExecutionState : PlayerBaseState
    {
        private readonly Transform target;
        private float stateTimer;
        private bool damageDealt;

        public PlayerExecutionState(StateMachine stateMachine, PlayerController player, Transform target)
            : base(stateMachine, player)
        {
            this.target = target;
        }

        public override void Enter()
        {
            stateTimer = 0f;
            damageDealt = false;
            SnapToExecutionPosition();
            PlayExecutionAnimation();
        }

        private void SnapToExecutionPosition()
        {
            if (target == null) return;
            Vector3 forwardToBoss = (target.position - player.transform.position).normalized;
            forwardToBoss.y = 0f;
            player.transform.rotation = Quaternion.LookRotation(forwardToBoss);
        }

        private void PlayExecutionAnimation()
        {
            if (player.Animator == null) return;
            player.Animator.SetTrigger(AnimationConstants.HeavyAttack);
        }

        public override void Tick(float deltaTime)
        {
            stateTimer += deltaTime;

            if (stateTimer >= 0.4f && !damageDealt)
            {
                damageDealt = true;
                DealExecutionDamage();
            }

            if (stateTimer >= 1.2f)
            {
                stateMachine.ChangeState(new PlayerLocomotionState(stateMachine, player));
            }
        }

        private void DealExecutionDamage()
        {
            if (target == null) return;

            if (target.TryGetComponent(out IDamageable damageable))
            {
                DamagePayload payload = new DamagePayload(
                    150f,
                    0f,
                    15f,
                    player.transform.forward,
                    target.position + Vector3.up,
                    player.gameObject,
                    true
                );
                damageable.TakeDamage(payload);
            }

            EventBus.Raise(new CombatImpactEvent(
                target.position + Vector3.up,
                -player.transform.forward,
                150f,
                true,
                false
            ));
        }
    }
}
