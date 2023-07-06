using System.Collections.Generic;
using Tests.Rigidbody_Factory.Scripts;
using UnityEngine;

namespace Common.AbstractPool
{
    public abstract class AbstractPool<T> : IPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        private readonly Transform _parent;
        private readonly int _initialPoolSize;
        private readonly AbstractFactory<T> _poolAbstractFactory;
        
        private readonly Queue<T> _pooledObjects = new();
        private readonly List<T> _wanderingObjects = new();

        protected AbstractPool(AbstractFactory<T> poolAbstractFactory, Transform parent, int initialPoolSize)
        {
            _poolAbstractFactory = poolAbstractFactory;
            _parent = parent;
            _initialPoolSize = initialPoolSize;
        }

        public void Initialize()
        {
            for (var i = 0; i < _initialPoolSize; i++)
                ExpandPool();
        }
        
        public void ReleaseAll()
        {
            for (int i = _wanderingObjects.Count - 1; i >= 0; i--)
            {
                Release(_wanderingObjects[i]);
            }

            OnPoolDisable();
        }

        public void Release(T poolable)
        {
            poolable.Release();
            poolable.transform.SetParent(_parent);
            poolable.GameObject.SetActive(false);

            _wanderingObjects.Remove(poolable);
            _pooledObjects.Enqueue(poolable);
        }

        protected T GetObject()
        {
            if (_pooledObjects.Count == 0)
                ExpandPool();

            T poolable = _pooledObjects.Dequeue();
            _wanderingObjects.Add(poolable);

            poolable.GameObject.SetActive(true);

            return poolable;
        }

        protected virtual void OnPoolDisable()
        { }

        private void ExpandPool()
        {
            T poolable = _poolAbstractFactory.Construct();
            poolable.GameObject.transform.SetParent(_parent);
            poolable.SetPool(this);
            poolable.GameObject.SetActive(false);
            _pooledObjects.Enqueue(poolable);
        }
    }
}