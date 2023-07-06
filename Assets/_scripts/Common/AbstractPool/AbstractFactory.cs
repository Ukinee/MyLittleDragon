using UnityEngine;

namespace Common.AbstractPool
{
    public abstract class AbstractFactory<T> where T : MonoBehaviour, IPoolable<T>
    {
        //Тут вызывается инициализация ключевыми зависимостями, общими для всех объектов пула
        public abstract T Construct();
    }
}