using Common.Services.Pools.Interfaces;
using UnityEngine;

namespace Common.Services.Factories.Interfaces
{
    public interface IFactory<out T> : IFactory where T : MonoBehaviour, IPoolable<T>
    {
        public T Construct();
    }

    public interface IFactory
    {
    }
}