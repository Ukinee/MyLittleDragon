using UnityEngine;

namespace Tests.Rigidbody_Factory.Scripts
{
    public interface IPool<in T> where T : MonoBehaviour
    {
        public bool IsDisabled { get; }
        public void Release(T poolable);
    }
}