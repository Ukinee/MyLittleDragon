using Common.AbstractPool;
using Common.AssetProvider;
using UnityEngine;

namespace Tests.Rigidbody_Factory.Scripts
{
    public class CubePool 
    {
        private ExpandablePool<CustomCube> _pool;

        public CubePool(Transform parent, IAssetProvider assetProvider, float lifetime, int initialPoolSize = 0)
        {
            var factory = new CustomCubeFactory(parent, assetProvider, lifetime);
            _pool = new ExpandablePool<CustomCube>(factory, parent);

            _pool.Initialize(initialPoolSize);
        }

        public CubePool(Transform parent, CustomCubeFactory customCubeFactory, int initialPoolSize = 0) 
        {
            _pool = new ExpandablePool<CustomCube>(customCubeFactory, parent);
            
            _pool.Initialize(initialPoolSize);
        }

        public CustomCube GetCube(Vector3 at, Vector3 attractorPosition, float force)
        {
            CustomCube poolable = _pool.GetObject();
            
            poolable.Init(attractorPosition, force);
            poolable.transform.position = at;

            return poolable;
        }

        public void Release(CustomCube poolable)
        {
            _pool.Release(poolable);
        }

        public void ReleaseAll()
        {
            _pool.ForceReleaseAll();
        }
    }
}