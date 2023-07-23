using Common.AbstractPool;
using Common.AssetProvider;
using UnityEngine;

namespace Tests.Rigidbody_Factory.Scripts
{
    public class CustomCubeFactory : IFactory<CustomCube>
    {
        private readonly Transform _parent;
        private readonly IAssetProvider _assetProvider;
        
        private readonly float _lifetime;

        public CustomCubeFactory(Transform parent, IAssetProvider assetProvider, float lifetime)
        {
            _parent = parent;
            _assetProvider = assetProvider;
            _lifetime = lifetime;
        }
        
        public CustomCube Construct()
        {
            var cube = _assetProvider.GetAsset<CustomCube>(AssetPath.CustomCube);
            cube.transform.SetParent(_parent);
            
            cube.Construct(_lifetime);
            
            return cube;
        }
    }
}