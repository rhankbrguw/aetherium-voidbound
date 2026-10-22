using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aetherium.Core.Pools
{
    public class GenericObjectPool<T> where T : class, IPoolable
    {
        private readonly T prefab;
        private readonly Transform parentTransform;
        private readonly Func<T> factoryFunc;
        private readonly Queue<T> pool = new Queue<T>();

        public GenericObjectPool(T prefab, int initialCapacity, Transform parentTransform = null)
        {
            this.prefab = prefab;
            this.parentTransform = parentTransform;
            this.factoryFunc = null;
            Prewarm(initialCapacity);
        }

        public GenericObjectPool(Func<T> factoryFunc, int initialCapacity)
        {
            this.prefab = null;
            this.parentTransform = null;
            this.factoryFunc = factoryFunc;
            Prewarm(initialCapacity);
        }

        private void Prewarm(int capacity)
        {
            for (int i = 0; i < capacity; i++)
            {
                T instance = CreateInstance();
                SetInstanceState(instance, false);
                instance.IsPooled = true;
                pool.Enqueue(instance);
            }
        }

        public T Get(Vector3 position, Quaternion rotation)
        {
            T instance = pool.Count > 0 ? pool.Dequeue() : CreateInstance();
            PositionInstance(instance, position, rotation);
            SetInstanceState(instance, true);
            instance.IsPooled = false;
            instance.OnSpawnFromPool();
            return instance;
        }

        public void Return(T instance)
        {
            if (instance == null || instance.IsPooled)
            {
                return;
            }

            instance.OnReturnToPool();
            SetInstanceState(instance, false);
            instance.IsPooled = true;
            pool.Enqueue(instance);
        }

        private T CreateInstance()
        {
            if (factoryFunc != null) return factoryFunc();
            return UnityEngine.Object.Instantiate((prefab as MonoBehaviour) as UnityEngine.Object, parentTransform) as T;
        }

        private void SetInstanceState(T instance, bool active)
        {
            if (instance is Component comp && comp != null) comp.gameObject.SetActive(active);
        }

        private void PositionInstance(T instance, Vector3 pos, Quaternion rot)
        {
            if (instance is Component comp && comp != null) comp.transform.SetPositionAndRotation(pos, rot);
        }
    }
}
