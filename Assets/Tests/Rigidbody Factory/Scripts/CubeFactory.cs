using Common.AbstractPool;
using UnityEngine;

namespace Tests.Rigidbody_Factory.Scripts
{
    public class CubeFactory : AbstractFactory<CustomCube>
    {
        private readonly CustomCube _cubePrefab;

        public CubeFactory(CustomCube cubePrefab)
        {
            _cubePrefab = cubePrefab;
        }
        
        public override CustomCube Construct()
        {
            CustomCube customCube = Object.Instantiate(_cubePrefab);
            //Тут вызывается инициализация ключевыми зависимостями, общими для всех объектов пула
            
            return customCube;
        }
    }
}