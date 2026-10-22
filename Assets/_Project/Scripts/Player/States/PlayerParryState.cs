using UnityEngine;
using Aetherium.Core.Constants;
using Aetherium.Core.StateMachine;
using Aetherium.Player.Controller;
using Aetherium.Combat.Data;
using Aetherium.Combat.Interfaces;
using Aetherium.Core.Events;

namespace Aetherium.Player.States
{
    public class PlayerParryState : PlayerBaseState, IParryable
    {
        private float stateTimer;
        private bool isParryWindowOpen;

        public PlayerParryState(StateMachine stateMachine, PlayerController player)
            : base(stateMachine, player) { }

        public override void Enter()
        {
            stateTimer = 0f;
            isParryWindowOpen = true;
            PlayParryAnimation();
        }

        private void PlayParryAnimation()
        {
            if (player.Animator == null) return;
            player.Animator.SetTrigger(AnimationConstants.Parry);
        }

        public override void Tick(float deltaTime)
        {
            stateTimer += deltaTime;
            if (stateTimer > GameConstants.PARRY_WINDOW_DURATION)
            {
                isParryWindowOpen = false;
            }

            if (stateTimer >= 0.45f)
            {
                stateMachine.ChangeState(new PlayerLocomotionState(stateMachine, player));
            }
        }

        public bool TryParry(DamagePayload payload)
        {
            if (!isParryWindowOpen || payload.IsUnblockable)
            {
                return false;
            }

            bool isPerfectDeflect = stateTimer <= 0.15f;
            float postureDamage = isPerfectDeflect ? 60f : 40f;

            if (payload.Source != null && payload.Source.TryGetComponent(out IPostureHittable postureTarget))
            {
                postureTarget.TakePostureDamage(postureDamage);
            }

            EventBus.Raise(new CombatImpactEvent(
                payload.HitPoint,
                -payload.HitDirection,
                0f,
                true,
                true
            ));

            return true;
        }

        public override void Exit()
        {
            isParryWindowOpen = false;
        }
    }
}
