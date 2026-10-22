using UnityEngine;
using Aetherium.Core.Constants;
using Aetherium.Core.StateMachine;
using Aetherium.Player.Controller;

namespace Aetherium.Player.States
{
    public class PlayerDodgeState : PlayerBaseState
    {
        private Vector3 dodgeDirection;
        private float elapsedTime;
        private bool isInvulnerable;

        public bool IsInvulnerable => isInvulnerable;

        public PlayerDodgeState(StateMachine stateMachine, PlayerController player) 
            : base(stateMachine, player) { }

        public override void Enter()
        {
            elapsedTime = 0f;
            isInvulnerable = false;
            CalculateDodgeDirection();
            TriggerDodgeAnimation();
        }

        private void CalculateDodgeDirection()
        {
            Vector2 input = player.Input != null ? player.Input.MoveInput : Vector2.zero;
            if (input.sqrMagnitude > 0.01f && player.CameraTransform != null)
            {
                Vector3 forward = player.CameraTransform.forward;
                Vector3 right = player.CameraTransform.right;
                forward.y = 0f;
                right.y = 0f;
                dodgeDirection = (forward.normalized * input.y + right.normalized * input.x).normalized;
                player.transform.rotation = Quaternion.LookRotation(dodgeDirection);
            }
            else
            {
                dodgeDirection = player.transform.forward;
            }
        }

        private void TriggerDodgeAnimation()
        {
            if (player.Animator == null) return;
            player.Animator.SetTrigger(AnimationConstants.Dodge);
        }

        public override void Tick(float deltaTime)
        {
            elapsedTime += deltaTime;
            UpdateIFrameStatus();
            ApplyDodgeMovement(deltaTime);

            float duration = player.Stats != null ? player.Stats.DodgeDuration : 0.45f;
            if (elapsedTime >= duration)
            {
                stateMachine.ChangeState(new PlayerLocomotionState(stateMachine, player));
            }
        }

        private void UpdateIFrameStatus()
        {
            isInvulnerable = elapsedTime >= GameConstants.DODGE_IFRAME_START && 
                             elapsedTime <= GameConstants.DODGE_IFRAME_END;
        }

        private void ApplyDodgeMovement(float deltaTime)
        {
            float maxSpeed = player.Stats != null ? player.Stats.DodgeSpeed : 11.0f;
            float duration = player.Stats != null ? player.Stats.DodgeDuration : 0.45f;
            float speedCurve = Mathf.Lerp(maxSpeed, 0f, elapsedTime / duration);
            Vector3 movement = (dodgeDirection * speedCurve) + (Vector3.up * -2f);
            player.CharacterController?.Move(movement * deltaTime);
        }

        public override void Exit()
        {
            isInvulnerable = false;
        }
    }
}
