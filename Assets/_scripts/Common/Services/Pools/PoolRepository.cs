using System;
using System.Collections.Generic;
using Common.Services.Pools.Interfaces;
using UnityEngine;

namespace Common.Services.Pools
{
    public class PoolRepository
    {
        private Dictionary<Type, IPool> _pools = new();

        public T Get<T>() where T : MonoBehaviour, IPoolable<T>
        {
            IPool<T> pool = GetPool<T>();

            return pool.GetObject();
        }

        public void Release<T>(T obj) where T : MonoBehaviour, IPoolable<T>
        {
            IPool<T> pool = GetPool<T>();

            pool.Release(obj);
        }

        public void Register<T>(IPool pool) where T : MonoBehaviour, IPoolable<T>
        {
            _pools[typeof(T)] = pool;
        }

        public void ReleaseAll<T>() where T : MonoBehaviour, IPoolable<T>
        {
            IPool<T> pool = GetPool<T>();

            pool.ForceReleaseAll();
        }

        private IPool<T> GetPool<T>() where T : MonoBehaviour, IPoolable<T>
        {
            if (_pools.TryGetValue(typeof(T), out IPool pool) == false)
                throw new ArgumentException("Pool not found");

            if (pool is null)
                throw new ArgumentException("Pool is null");

            if (pool is not IPool<T> foundPool)
                throw new ArgumentException("Found pool is not of type " + typeof(T));

            return foundPool;
        }
    }
}