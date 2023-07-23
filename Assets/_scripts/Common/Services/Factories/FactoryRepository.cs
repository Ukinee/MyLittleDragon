using System;
using System.Collections.Generic;
using Common.Services.Factories.Interfaces;
using Common.Services.Pools.Interfaces;
using UnityEngine;

namespace Common.Services.Factories
{
    public class FactoryRepository
    {
        private Dictionary<Type, IFactory> _pools = new();

        public IFactory<T> Get<T>() where T : MonoBehaviour, IPoolable<T>
        {
            IFactory<T> pool = GetFactory<T>();

            return pool;
        }

        public void Register<T>(IFactory pool) where T : MonoBehaviour, IPoolable<T>
        {
            _pools[typeof(T)] = pool;
        }

        private IFactory<T> GetFactory<T>() where T : MonoBehaviour, IPoolable<T>
        {
            if (_pools.TryGetValue(typeof(T), out IFactory pool) == false)
                throw new ArgumentException("Factory not found");

            if (pool is null)
                throw new ArgumentException("Factory is null");

            if (pool is not IFactory<T> foundPool)
                throw new ArgumentException("Factory pool is not of type " + typeof(T));

            return foundPool;
        }
    }
}