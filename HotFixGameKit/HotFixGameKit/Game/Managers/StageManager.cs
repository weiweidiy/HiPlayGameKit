using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace HotFixGameKit.Game
{

    public class StageManager
    {
        ISceneLoaderAsync _sceneLoader;

        IGameObjectLoaderAsync _gameObjectLoader;

        public StageManager(ISceneLoaderAsync sceneLoader , IGameObjectLoaderAsync gameObjectLoader)
        {
            _sceneLoader = sceneLoader;
            _gameObjectLoader = gameObjectLoader;
        }

        /// <summary>
        /// 加载Unity场景
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="loadSceneMode"></param>
        /// <param name="activeOnLoad"></param>
        /// <returns></returns>
        public IAsyncHandle LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single, bool activeOnLoad = true)
        {
            return _sceneLoader.LoadSceneAsync(sceneName, loadSceneMode, activeOnLoad);
        }

        /// <summary>
        /// 创建一个游戏对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public IAsyncHandle CreateGameObjectAsync(string key, Transform parent = null)
        {
            var handle = _gameObjectLoader.InstantiatorAsync(key, parent) as UnityAsyncHandle;
            handle.Complete += (sender, args) =>
            {
                var go = (args as ObjectEventArgs).Result as GameObject;
                Debug.Log("create complete" + go.name);
            };
            return handle;
        }
        
    }
}
