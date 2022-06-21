using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HotFixGameKit.Game
{
    public class AssetLoaderAddressables : IObjectLoaderAsync , IGameObjectLoaderAsync
    {
        public IAsyncHandle InstantiatorAsync(string key,Transform parent = null)
        {
            var internalHandle = Addressables.InstantiateAsync(key, parent);
            return new UnityAsyncHandle(internalHandle);
        }

        public IAsyncHandle InstantiatorAsync(string key, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var internalHandle = Addressables.InstantiateAsync(key, position, rotation, parent);
            return new UnityAsyncHandle(internalHandle);
        }

        public IAsyncHandle LoadObjectAsync(string assetName)
        {
            var internalHandle = Addressables.LoadAssetAsync<object>(assetName);
            return new UnityAsyncHandle(internalHandle);
        }

        public IAsyncHandle LoadObjectAsync<T>(string assetName)
        {
            var internalHandle = Addressables.LoadAssetAsync<T>(assetName);
            return new UnityAsyncHandle(internalHandle);
        }
    }
}
