using System.Collections.Generic;
using UnityEngine;
using Aetherium.Combat.Data;
using Aetherium.Combat.Interfaces;
using Aetherium.Core.Constants;

namespace Aetherium.Combat.Hitbox
{
    public class HitboxController : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private Vector3 boxHalfExtents = new Vector3(0.5f, 0.5f, 1.0f);

        private readonly Collider[] hitBuffer = new Collider[GameConstants.NON_ALLOC_COLLIDER_BUFFER_SIZE];
        private readonly HashSet<Collider> alreadyHit = new HashSet<Collider>();
        private AttackDataSO currentAttack;
        private GameObject owner;
        private bool isActive;

        public void Initialize(GameObject owner)
        {
            this.owner = owner;
        }

        public void EnableHitbox(AttackDataSO attackData)
        {
            currentAttack = attackData;
            alreadyHit.Clear();
            isActive = true;
        }

        public void DisableHitbox()
        {
            isActive = false;
            alreadyHit.Clear();
            currentAttack = null;
        }

        private void FixedUpdate()
        {
            if (!isActive || currentAttack == null)
            {
                return;
            }

            PerformSpatialSweep();
        }

        private void PerformSpatialSweep()
        {
            int hitCount = Physics.OverlapBoxNonAlloc(
                transform.position,
                boxHalfExtents,
                hitBuffer,
                transform.rotation,
                targetLayer,
                QueryTriggerInteraction.Collide
            );

            for (int i = 0; i < hitCount; i++)
            {
                ProcessHitCollider(hitBuffer[i]);
            }
        }

        private void ProcessHitCollider(Collider target)
        {
            if (target == null || alreadyHit.Contains(target) || (owner != null && target.gameObject == owner))
            {
                return;
            }

            alreadyHit.Add(target);
            if (target.TryGetComponent(out IDamageable damageable))
            {
                Vector3 hitPoint = target.ClosestPoint(transform.position);
                Vector3 hitDirection = (target.transform.position - transform.position).normalized;
                DamagePayload payload = new DamagePayload(
                    currentAttack.Damage,
                    currentAttack.PostureDamage,
                    currentAttack.KnockbackForce,
                    hitDirection,
                    hitPoint,
                    owner,
                    currentAttack.IsUnblockable
                );
                damageable.TakeDamage(payload);
            }
        }
    }
}
