using UnityEngine;

namespace Tests.Rigidbody_Factory.Scripts
{
    public class CubeProducer : MonoBehaviour
    {
        [SerializeField] private CustomCube _cubePrefab;
        [SerializeField] private Transform _poolRoot;
        [SerializeField] private Transform _attractor;
        [SerializeField] private float _force;
        [SerializeField] private float _lifetime;

        private CubePool _pool;

        private void Awake()
        {
            // сюда могут пойти еще какие то зависимости, поэтому фабрика создается тут
            // чтобы не прокидывать через пул
            CubeFactory factory = new(_cubePrefab); 
            _pool = new CubePool(factory, _poolRoot, 10);
            _pool.Initialize();
        }

        public void SpawnCube()
        {
            _pool.GetCube(
                transform.position,
                _attractor.position,
                Random.Range(-_force, _force),
                _lifetime);
        }

        public void ReleaseAll()
        {
            _pool.ReleaseAll();
        }
    }
}