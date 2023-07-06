using Tests.Rigidbody_Factory.Scripts;
using UnityEngine;

namespace Common.AbstractPool
{
    public interface IPoolable<out T> where T : MonoBehaviour, IPoolable<T>
    {
        public GameObject GameObject { get; }

        public void Release();
        public void SetPool(IPool<T> ownerPool);
    }
}