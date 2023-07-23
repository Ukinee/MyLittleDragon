using UnityEngine;

namespace Common.Services.Pools.Interfaces
{
    public interface IPoolable<T> where T : MonoBehaviour, IPoolable<T>
    {
        public GameObject GameObject { get; }

        public void Release();
        public void SetPool(IPool<T> ownerPool);
    }
}