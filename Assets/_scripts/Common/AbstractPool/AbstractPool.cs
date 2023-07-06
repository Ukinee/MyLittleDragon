using System.Collections.Generic;
using _scripts.AbstractPool;
using _scripts.Common.AbstractPool;
using Tests.Rigidbody_Factory.Scripts;
using UnityEngine;

namespace Common.AbstractPool
{
    public abstract class AbstractPool<T> : MonoBehaviour, IPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        [SerializeField] private int _initialPoolSize;
        [SerializeField] private T _poolablePrefab;

        private readonly Queue<T> _pooledObjects = new();
        private readonly List<T> _wanderingObjects = new();

        public bool IsDisabled { get; private set; }

        private void Awake()
        {
            Initialize();
        }

        public T GetObject()
        {
            if (IsDisabled)
                return null;

            if (_pooledObjects.Count == 0)
                ExpandPool();

            T poolable = _pooledObjects.Dequeue();
            _wanderingObjects.Add(poolable);

            poolable.GameObject.SetActive(true);
            poolable.Init(this, GetInitData());

            return poolable;
        }

        public void Release(T poolable)
        {
            poolable.Release(GetReleaseData());
            poolable.transform.SetParent(transform);
            poolable.GameObject.SetActive(false);

            _wanderingObjects.Remove(poolable);
            _pooledObjects.Enqueue(poolable);
        }

        public void Disable()
        {
            for (int i = _wanderingObjects.Count - 1; i >= 0; i--)
            {
                Release(_wanderingObjects[i]);
            }

            IsDisabled = true;
            OnPoolDisable();
        }

        protected virtual void OnPoolDisable()
        { }
        
        protected abstract IPoolReleaseData GetReleaseData();
        protected abstract IPoolInitializationData GetInitData();

        private void ExpandPool()
        {
            T poolable = Instantiate(_poolablePrefab, transform);
            poolable.GameObject.SetActive(false);
            _pooledObjects.Enqueue(poolable);
        }

        private void Initialize()
        {
            for (var i = 0; i < _initialPoolSize; i++)
                ExpandPool();
        }
    }
}