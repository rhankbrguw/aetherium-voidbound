using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Aetherium.Player.Controller
{
    [CreateAssetMenu(fileName = "PlayerInputReader", menuName = "Aetherium/Input/Player Input Reader")]
    public class PlayerInputReader : ScriptableObject
    {
        public event Action<Vector2> MoveEvent;
        public event Action<Vector2> LookEvent;
        public event Action AttackEvent;
        public event Action HeavyAttackEvent;
        public event Action ParryEvent;
        public event Action DodgeEvent;
        public event Action<bool> SprintEvent;
        public event Action LockOnEvent;

        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool IsSprinting { get; private set; }

        public void PollInput()
        {
            PollMovementInput();
            PollActionButtons();
        }

        private void PollMovementInput()
        {
            Vector2 move = Vector2.zero;
            Keyboard kb = Keyboard.current;
            if (kb != null)
            {
                if (kb.wKey.isPressed || kb.upArrowKey.isPressed) move.y += 1f;
                if (kb.sKey.isPressed || kb.downArrowKey.isPressed) move.y -= 1f;
                if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) move.x += 1f;
                if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) move.x -= 1f;
                IsSprinting = kb.leftShiftKey.isPressed || kb.rightShiftKey.isPressed;
            }
            MoveInput = move.normalized;
        }

        private void PollActionButtons()
        {
            Mouse mouse = Mouse.current;
            if (mouse != null)
            {
                if (mouse.leftButton.wasPressedThisFrame) TriggerAttack();
                if (mouse.rightButton.wasPressedThisFrame) TriggerParry();
                if (mouse.middleButton.wasPressedThisFrame) TriggerLockOn();
            }

            Keyboard kb = Keyboard.current;
            if (kb != null)
            {
                if (kb.spaceKey.wasPressedThisFrame) TriggerDodge();
                if (kb.tabKey.wasPressedThisFrame) TriggerLockOn();
            }
        }

        public void TriggerAttack() => AttackEvent?.Invoke();
        public void TriggerHeavyAttack() => HeavyAttackEvent?.Invoke();
        public void TriggerParry() => ParryEvent?.Invoke();
        public void TriggerDodge() => DodgeEvent?.Invoke();
        public void TriggerLockOn() => LockOnEvent?.Invoke();
    }
}
