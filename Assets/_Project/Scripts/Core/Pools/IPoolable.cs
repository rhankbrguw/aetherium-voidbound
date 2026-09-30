namespace Aetherium.Core.Pools
{
    public interface IPoolable
    {
        bool IsPooled { get; set; }
        void OnSpawnFromPool();
        void OnReturnToPool();
    }
}
