using UnityEngine;
using Aetherium.Core.Events;
using Aetherium.Core.Pools;

namespace Aetherium.AudioVFX
{
    public class VFXManager : MonoBehaviour
    {
        [SerializeField] private PoolableObject parrySparkPrefab;
        [SerializeField] private PoolableObject swordHitPrefab;
        [SerializeField] private int initialPoolSize = 10;

        private GenericObjectPool<PoolableObject> parryPool;
        private GenericObjectPool<PoolableObject> hitPool;

        private void Awake()
        {
            InitializePools();
        }

        private void InitializePools()
        {
            if (parrySparkPrefab != null)
            {
                parryPool = new GenericObjectPool<PoolableObject>(parrySparkPrefab, initialPoolSize, transform);
            }
            if (swordHitPrefab != null)
            {
                hitPool = new GenericObjectPool<PoolableObject>(swordHitPrefab, initialPoolSize, transform);
            }
        }

        private void OnEnable()
        {
            EventBus.Subscribe<CombatImpactEvent>(HandleCombatImpact);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<CombatImpactEvent>(HandleCombatImpact);
        }

        private void HandleCombatImpact(CombatImpactEvent impact)
        {
            Quaternion rotation = impact.HitNormal != Vector3.zero 
                ? Quaternion.LookRotation(impact.HitNormal) 
                : Quaternion.identity;

            if (impact.IsParry && parryPool != null)
            {
                parryPool.Get(impact.HitPosition, rotation);
            }
            else if (hitPool != null)
            {
                hitPool.Get(impact.HitPosition, rotation);
            }
        }
    }
}
