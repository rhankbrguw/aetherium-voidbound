using UnityEngine;
using Aetherium.Core.Constants;
using Aetherium.Core.StateMachine;
using Aetherium.Player.Controller;

namespace Aetherium.Player.States
{
    public class PlayerLocomotionState : PlayerBaseState
    {
        private float verticalVelocity;

        public PlayerLocomotionState(StateMachine stateMachine, PlayerController player) 
            : base(stateMachine, player) { }

        public override void Enter()
        {
            SubscribeInput();
        }

        public override void Exit()
        {
            UnsubscribeInput();
        }

        public override void Tick(float deltaTime)
        {
            CheckVoidFall();
            Vector2 input = player.Input != null ? player.Input.MoveInput : Vector2.zero;
            Vector3 moveDirection = CalculateMovementDirection(input);
            float walkSpeed = player.Stats != null ? player.Stats.WalkSpeed : 4.0f;
            float runSpeed = player.Stats != null ? player.Stats.RunSpeed : 7.5f;
            bool isSprinting = player.Input != null && player.Input.IsSprinting;
            float targetSpeed = input.magnitude > 0.1f ? (isSprinting ? runSpeed : walkSpeed) : 0f;

            ApplyRotation(moveDirection, deltaTime);
            ApplyMovement(moveDirection, targetSpeed, deltaTime);
            UpdateAnimator(targetSpeed);
        }

        private void CheckVoidFall()
        {
            if (player.transform.position.y < -10f)
            {
                if (player.CharacterController != null) player.CharacterController.enabled = false;
                player.transform.position = new Vector3(0f, 1.0f, -6f);
                if (player.CharacterController != null) player.CharacterController.enabled = true;
                verticalVelocity = 0f;
            }
        }

        private Vector3 CalculateMovementDirection(Vector2 input)
        {
            if (input.sqrMagnitude < 0.01f || player.CameraTransform == null)
            {
                return new Vector3(input.x, 0f, input.y).normalized;
            }

            Vector3 forward = player.CameraTransform.forward;
            Vector3 right = player.CameraTransform.right;
            forward.y = 0f;
            right.y = 0f;
            return (forward.normalized * input.y + right.normalized * input.x).normalized;
        }

        private void ApplyRotation(Vector3 direction, float deltaTime)
        {
            if (direction.sqrMagnitude < 0.01f)
            {
                return;
            }

            float rotSpeed = player.Stats != null ? player.Stats.RotationSpeed : 12.0f;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            player.transform.rotation = Quaternion.Slerp(
                player.transform.rotation, 
                targetRotation, 
                rotSpeed * deltaTime
            );
        }

        private void ApplyMovement(Vector3 direction, float speed, float deltaTime)
        {
            if (player.CharacterController == null) return;

            if (player.CharacterController.isGrounded)
            {
                verticalVelocity = -2f;
            }
            else
            {
                verticalVelocity += GameConstants.DEFAULT_GRAVITY * deltaTime;
            }

            Vector3 movement = (direction * speed) + (Vector3.up * verticalVelocity);
            player.CharacterController.Move(movement * deltaTime);
        }

        private void UpdateAnimator(float speed)
        {
            if (player.Animator == null) return;
            player.Animator.SetFloat(AnimationConstants.Speed, speed, 0.1f, Time.deltaTime);
            if (player.CharacterController != null)
            {
                player.Animator.SetBool(AnimationConstants.IsGrounded, player.CharacterController.isGrounded);
            }
        }

        private void SubscribeInput()
        {
            if (player.Input == null) return;
            player.Input.DodgeEvent += OnDodgeTriggered;
            player.Input.AttackEvent += OnAttackTriggered;
            player.Input.ParryEvent += OnParryTriggered;
        }

        private void UnsubscribeInput()
        {
            if (player.Input == null) return;
            player.Input.DodgeEvent -= OnDodgeTriggered;
            player.Input.AttackEvent -= OnAttackTriggered;
            player.Input.ParryEvent -= OnParryTriggered;
        }

        private void OnDodgeTriggered()
        {
            float cost = player.Stats != null ? player.Stats.DodgeStaminaCost : 20f;
            if (player.ConsumeStamina(cost))
            {
                stateMachine.ChangeState(new PlayerDodgeState(stateMachine, player));
            }
        }

        private void OnAttackTriggered()
        {
            stateMachine.ChangeState(new PlayerAttackState(stateMachine, player));
        }

        private void OnParryTriggered()
        {
            stateMachine.ChangeState(new PlayerParryState(stateMachine, player));
        }
    }
}
