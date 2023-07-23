using UnityEngine;

namespace Common.AbstractPool
{
    public interface IPool<in T> where T : MonoBehaviour, IPoolable<T>
    {
        public void Release(T poolable);
    }
}