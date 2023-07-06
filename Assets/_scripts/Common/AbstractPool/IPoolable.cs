using _scripts.AbstractPool;
using _scripts.Common.AbstractPool;
using Tests.Rigidbody_Factory.Scripts;
using UnityEngine;

namespace Common.AbstractPool
{
    public interface IPoolable<out T> where T : MonoBehaviour, IPoolable<T>
    {
        public GameObject GameObject { get; }

        public void Release(IPoolReleaseData data);
        public void Init(IPool<T> ownerPool, IPoolInitializationData data);
    }
}