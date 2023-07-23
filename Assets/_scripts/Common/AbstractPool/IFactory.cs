using UnityEngine;

namespace Common.AbstractPool
{
    public interface IFactory<out T> where T : MonoBehaviour, IPoolable<T>
    {
        public T Construct();
    }
}