using Common.AssetProvider;
using UnityEngine;

namespace Tests.Rigidbody_Factory.Scripts
{
    public class CubeProducer : MonoBehaviour
    {
        [SerializeField] private Transform _poolRoot;
        [SerializeField] private Transform _attractor;
        [SerializeField] private float _lifetime;
        [SerializeField] private float _force;

        private CubePool _pool;
        
        private void Awake()
        {
            IAssetProvider assetProvider = new AssetProvider();
            
            _pool = new CubePool(_poolRoot, assetProvider, _lifetime, 5);
        }

        public void SpawnCube()
        {
            _pool.GetCube(transform.position, _attractor.position, _force);
        }

        public void ReleaseAll()
        {
            _pool.ReleaseAll();
        }
    }
}