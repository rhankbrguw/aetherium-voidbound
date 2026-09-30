using UnityEngine;

namespace Aetherium.Core.Events
{
    public readonly struct PlayerHealthChangedEvent
    {
        public readonly float CurrentHealth;
        public readonly float MaxHealth;

        public PlayerHealthChangedEvent(float currentHealth, float maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }
    }

    public readonly struct PlayerStaminaChangedEvent
    {
        public readonly float CurrentStamina;
        public readonly float MaxStamina;

        public PlayerStaminaChangedEvent(float currentStamina, float maxStamina)
        {
            CurrentStamina = currentStamina;
            MaxStamina = maxStamina;
        }
    }

    public readonly struct BossHealthChangedEvent
    {
        public readonly float CurrentHealth;
        public readonly float MaxHealth;
        public readonly int CurrentPhase;

        public BossHealthChangedEvent(float currentHealth, float maxHealth, int currentPhase)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
            CurrentPhase = currentPhase;
        }
    }

    public readonly struct BossPostureChangedEvent
    {
        public readonly float CurrentPosture;
        public readonly float MaxPosture;
        public readonly bool IsGroggy;

        public BossPostureChangedEvent(float currentPosture, float maxPosture, bool isGroggy)
        {
            CurrentPosture = currentPosture;
            MaxPosture = maxPosture;
            IsGroggy = isGroggy;
        }
    }

    public readonly struct CombatImpactEvent
    {
        public readonly Vector3 HitPosition;
        public readonly Vector3 HitNormal;
        public readonly float DamageAmount;
        public readonly bool IsCritical;
        public readonly bool IsParry;

        public CombatImpactEvent(Vector3 hitPosition, Vector3 hitNormal, float damageAmount, bool isCritical, bool isParry)
        {
            HitPosition = hitPosition;
            HitNormal = hitNormal;
            DamageAmount = damageAmount;
            IsCritical = isCritical;
            IsParry = isParry;
        }
    }

    public readonly struct SoundEffectEvent
    {
        public readonly string ClipKey;
        public readonly Vector3 Position;
        public readonly float Volume;

        public SoundEffectEvent(string clipKey, Vector3 position, float volume)
        {
            ClipKey = clipKey;
            Position = position;
            Volume = volume;
        }
    }
}
