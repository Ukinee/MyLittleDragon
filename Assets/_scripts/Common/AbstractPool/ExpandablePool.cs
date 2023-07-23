using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.AbstractPool
{
    public class ExpandablePool<T> : IPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        private readonly IFactory<T> _factory;
        private readonly Transform _parent;

        private readonly Queue<T> _pooledObjects = new();
        private readonly List<T> _wanderingObjects = new();

        public ExpandablePool(IFactory<T> factory, Transform parent)
        {
            _factory = factory;
            _parent = parent;
        }

        public void Release(T poolable)
        {
            poolable.Release();
            poolable.transform.SetParent(_parent);
            poolable.GameObject.SetActive(false);

            _wanderingObjects.Remove(poolable);
            _pooledObjects.Enqueue(poolable);
        }

        public T GetObject()
        {
            if (_pooledObjects.Count == 0)
                ExpandPool();

            T poolable = _pooledObjects.Dequeue();
            _wanderingObjects.Add(poolable);

            poolable.GameObject.SetActive(true);

            return poolable;
        }
        
        public void ApplyToAllObjects(Action<T> action)
        {
            foreach (T poolable in _pooledObjects)
                action.Invoke(poolable);
            
            foreach (T poolable in _wanderingObjects)
                action.Invoke(poolable);
        }

        public void ForceReleaseAll()
        {
            for (int i = _wanderingObjects.Count - 1; i >= 0; i--)
            {
                Release(_wanderingObjects[i]);
            }
        }

        private void ExpandPool()
        {
            T poolable = _factory.Construct();
            poolable.GameObject.SetActive(false);
            poolable.SetPool(this);
            _pooledObjects.Enqueue(poolable);
        }

        public void Initialize(int size)
        {
            for (int i = 0; i < size; i++)
                ExpandPool();
        }
    }
}