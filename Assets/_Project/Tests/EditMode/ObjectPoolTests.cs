using NUnit.Framework;
using UnityEngine;
using Aetherium.Core.Pools;

namespace Aetherium.Tests.EditMode
{
    public class ObjectPoolTests
    {
        private class TestPoolItem : IPoolable
        {
            public bool IsPooled { get; set; }
            public int SpawnCount { get; private set; }
            public int ReturnCount { get; private set; }

            public void OnSpawnFromPool() => SpawnCount++;
            public void OnReturnToPool() => ReturnCount++;
        }

        [Test]
        public void ObjectPool_GetAndReturn_RecyclesInstances()
        {
            GenericObjectPool<TestPoolItem> pool = new GenericObjectPool<TestPoolItem>(() => new TestPoolItem(), 1);

            TestPoolItem item1 = pool.Get(Vector3.zero, Quaternion.identity);
            Assert.IsNotNull(item1);
            Assert.AreEqual(1, item1.SpawnCount);
            Assert.IsFalse(item1.IsPooled);

            pool.Return(item1);
            Assert.AreEqual(1, item1.ReturnCount);
            Assert.IsTrue(item1.IsPooled);

            TestPoolItem reusedItem = pool.Get(Vector3.zero, Quaternion.identity);
            Assert.AreSame(item1, reusedItem);
            Assert.AreEqual(2, reusedItem.SpawnCount);
        }
    }
}
