using UnityEngine;

namespace Aetherium.Core.Pools
{
    public abstract class PoolableObject : MonoBehaviour, IPoolable
    {
        public bool IsPooled { get; set; }

        public virtual void OnSpawnFromPool() { }

        public virtual void OnReturnToPool() { }
    }
}
