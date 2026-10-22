using System;
using UnityEngine;
using Aetherium.Combat.Data;
using Aetherium.Combat.Interfaces;
using Aetherium.Core.Events;

namespace Aetherium.Combat.Hitbox
{
    public class Hurtbox : MonoBehaviour, IDamageable, IPostureHittable
    {
        [SerializeField] private bool isInvulnerable;
        [SerializeField] private float damageMultiplier = 1.0f;

        public event Action<DamagePayload> OnDamaged;
        public event Action<float> OnPostureDamaged;

        public bool IsInvulnerable
        {
            get => isInvulnerable;
            set => isInvulnerable = value;
        }

        public void TakeDamage(DamagePayload payload)
        {
            if (isInvulnerable)
            {
                return;
            }

            if (TryGetComponent(out IParryable parryable) && parryable.TryParry(payload))
            {
                return;
            }

            float finalDamage = payload.DamageAmount * damageMultiplier;
            OnDamaged?.Invoke(payload);

            EventBus.Raise(new CombatImpactEvent(
                payload.HitPoint,
                -payload.HitDirection,
                finalDamage,
                false,
                false
            ));
        }

        public void TakePostureDamage(float amount)
        {
            if (isInvulnerable)
            {
                return;
            }

            OnPostureDamaged?.Invoke(amount);
        }
    }
}
