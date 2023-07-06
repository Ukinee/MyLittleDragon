using UnityEngine;

namespace Tests.Rigidbody_Factory.Scripts
{
    public interface IPool<in T> where T : MonoBehaviour
    {
        public void Release(T poolable);
    }
}