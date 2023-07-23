using System;
using UnityEngine;

namespace Common.Services.Pools.Interfaces
{
    public interface IPool<T> : IPool where T : MonoBehaviour, IPoolable<T>
    {
        public T GetObject();
        public void Release(T poolable);
        public void ApplyToAllObjects(Action<T> action);
        public void ForceReleaseAll();
    }

    public interface IPool
    {
    }
}