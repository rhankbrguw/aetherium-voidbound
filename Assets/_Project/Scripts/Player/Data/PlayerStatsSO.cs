using UnityEngine;

namespace Aetherium.Player.Data
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Aetherium/Player/Player Stats")]
    public class PlayerStatsSO : ScriptableObject
    {
        [Header("Health & Vitality")]
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float maxStamina = 100f;
        [SerializeField] private float staminaRegenRate = 25f;
        [SerializeField] private float staminaRegenDelay = 1.0f;

        [Header("Locomotion")]
        [SerializeField] private float walkSpeed = 4.0f;
        [SerializeField] private float runSpeed = 7.5f;
        [SerializeField] private float rotationSpeed = 12.0f;

        [Header("Dodge & I-Frames")]
        [SerializeField] private float dodgeSpeed = 11.0f;
        [SerializeField] private float dodgeDuration = 0.45f;
        [SerializeField] private float dodgeStaminaCost = 20.0f;

        public float MaxHealth => maxHealth;
        public float MaxStamina => maxStamina;
        public float StaminaRegenRate => staminaRegenRate;
        public float StaminaRegenDelay => staminaRegenDelay;
        public float WalkSpeed => walkSpeed;
        public float RunSpeed => runSpeed;
        public float RotationSpeed => rotationSpeed;
        public float DodgeSpeed => dodgeSpeed;
        public float DodgeDuration => dodgeDuration;
        public float DodgeStaminaCost => dodgeStaminaCost;
    }
}
