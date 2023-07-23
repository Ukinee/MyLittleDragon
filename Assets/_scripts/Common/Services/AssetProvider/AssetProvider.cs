using Common.AssetProvider;
using UnityEngine;

namespace Common.Services.AssetProvider
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject GetAsset(string assetPath)
        {
            var asset = Resources.Load<GameObject>(assetPath);

            asset = Object.Instantiate(asset);
            
            return asset;
        }

        public T GetAsset<T>(string assetPath) where T : MonoBehaviour
        {
            var asset = Resources.Load<T>(assetPath);

            asset = Object.Instantiate(asset);
            
            return asset;
        }
    }
}