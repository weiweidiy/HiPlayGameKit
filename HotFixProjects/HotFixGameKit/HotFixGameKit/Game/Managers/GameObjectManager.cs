using UnityEngine;

namespace HotFixGameKit.Game
{


    public class GameObjectManager
    {
        IGameObjectLoaderAsync _gameObjectLoader;

        public GameObjectManager(IGameObjectLoaderAsync gameObjectLoader)
        {
            _gameObjectLoader = gameObjectLoader;
        }

        /// <summary>
        /// 创建一个游戏对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public UnityAsyncHandle CreateGameObjectAsync(string key, Transform parent = null)
        {
            var handle = _gameObjectLoader.InstantiatorAsync(key, parent) as UnityAsyncHandle;
            handle.Complete += (sender, args) =>
            {
                var go = (args as ObjectEventArgs).Result as GameObject;
            };
            return handle;
        }
    }
}
