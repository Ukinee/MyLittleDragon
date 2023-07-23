using Common.AssetProvider;
using Common.Services.AssetProvider;
using Common.Services.Factories;
using Common.Services.Factories.Interfaces;
using Common.Services.Pools;
using Common.Services.Pools.Interfaces;
using UnityEngine;

namespace Tests.Rigidbody_Factory.Scripts
{
    public class CubeProducer : MonoBehaviour
    {
        [Header("Awake")] [SerializeField] private float _lifetime;
        [SerializeField] private Transform _poolRoot;

        [Header("Runtime")] [SerializeField] private Transform _attractor;
        [SerializeField] private float _force;
        [SerializeField] private float _oscillateSpeed;

        private PoolRepository _poolRepository;
        private FactoryRepository _factoryRepository;

        private void Awake()
        {
            IAssetProvider assetProvider = new AssetProvider();

            IFactory<CustomCube> factory = new CustomCubeFactory(_poolRoot, assetProvider, _lifetime);
            IPool pool = new ExpandablePool<CustomCube>(factory, _poolRoot);

            _poolRepository = new PoolRepository();
            _factoryRepository = new FactoryRepository();

            _poolRepository.Register<CustomCube>(pool);
            _factoryRepository.Register<CustomCube>(factory);
        }

        public void SpawnCube()
        {
            var customCube = _poolRepository.Get<CustomCube>();
            customCube.Init(_attractor.position, _force, _oscillateSpeed);
            Transform cubeTransform = customCube.transform;

            cubeTransform.position = transform.position;
            cubeTransform.rotation = Quaternion.identity;
        }

        public void ReleaseAll()
        {
            _poolRepository.ReleaseAll<CustomCube>();
        }
    }
}