using UnityEngine;

namespace Aetherium.Combat.Data
{
    [CreateAssetMenu(fileName = "NewAttackData", menuName = "Aetherium/Combat/Attack Data")]
    public class AttackDataSO : ScriptableObject
    {
        [Header("Damage & Posture")]
        [SerializeField] private float damage = 25f;
        [SerializeField] private float postureDamage = 15f;
        [SerializeField] private float knockbackForce = 5f;
        [SerializeField] private float staminaCost = 15f;
        [SerializeField] private bool isUnblockable = false;

        [Header("Frame Data & Timing")]
        [SerializeField] private float startupDuration = 0.1f;
        [SerializeField] private float activeHitboxDuration = 0.2f;
        [SerializeField] private float recoveryDuration = 0.3f;
        [SerializeField] private float comboWindowStart = 0.2f;

        [Header("Juice & Feedback")]
        [SerializeField] private float hitStopDuration = 0.06f;
        [SerializeField] private float cameraShakeIntensity = 0.3f;
        [SerializeField] private string hitSoundKey = "SFX_SwordHit_Flesh";

        public float Damage => damage;
        public float PostureDamage => postureDamage;
        public float KnockbackForce => knockbackForce;
        public float StaminaCost => staminaCost;
        public bool IsUnblockable => isUnblockable;
        public float StartupDuration => startupDuration;
        public float ActiveHitboxDuration => activeHitboxDuration;
        public float RecoveryDuration => recoveryDuration;
        public float TotalDuration => startupDuration + activeHitboxDuration + recoveryDuration;
        public float ComboWindowStart => comboWindowStart;
        public float HitStopDuration => hitStopDuration;
        public float CameraShakeIntensity => cameraShakeIntensity;
        public string HitSoundKey => hitSoundKey;
    }
}
