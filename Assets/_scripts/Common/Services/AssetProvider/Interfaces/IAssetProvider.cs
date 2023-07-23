using UnityEngine;

namespace Common.AssetProvider
{
    public interface IAssetProvider
    {
        public GameObject GetAsset(string assetPath);
        public T GetAsset<T>(string assetPath) where T : MonoBehaviour;
    }
}