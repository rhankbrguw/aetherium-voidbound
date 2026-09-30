using UnityEngine;

namespace Aetherium.Combat.Data
{
    public readonly struct DamagePayload
    {
        public readonly float DamageAmount;
        public readonly float PostureDamage;
        public readonly float KnockbackForce;
        public readonly Vector3 HitDirection;
        public readonly Vector3 HitPoint;
        public readonly GameObject Source;
        public readonly bool IsUnblockable;

        public DamagePayload(
            float damageAmount, 
            float postureDamage, 
            float knockbackForce, 
            Vector3 hitDirection, 
            Vector3 hitPoint, 
            GameObject source, 
            bool isUnblockable = false)
        {
            DamageAmount = damageAmount;
            PostureDamage = postureDamage;
            KnockbackForce = knockbackForce;
            HitDirection = hitDirection;
            HitPoint = hitPoint;
            Source = source;
            IsUnblockable = isUnblockable;
        }
    }
}
