using UnityEngine;

namespace HotFixGameKit.Game
{

    public interface IGameObjectLoaderAsync
    {
        IAsyncHandle InstantiatorAsync(string key, Transform parent = null);

        IAsyncHandle InstantiatorAsync(string key, Vector3 position, Quaternion rotation, Transform parent = null);
    }
}
