using System.Collections.Generic;
using System.Text;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace HotFixGameKit.Game
{
    /// <summary>
    /// 使用addressable加载场景
    /// </summary>
    public class UnitySceneLoaderAddressables : ISceneLoaderAsync
    {
        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="key"></param>
        /// <param name="loadMode"></param>
        /// <param name="activateOnLoad"></param>
        /// <returns></returns>
        public IAsyncHandle LoadSceneAsync(string key, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true)
        {
            var internalHandle = Addressables.LoadSceneAsync(key, loadMode, activateOnLoad);
            return new UnityAsyncHandle(internalHandle);
        }



        /// <summary>
        /// 卸载场景
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public IAsyncHandle UnLoadSceneAsync(IAsyncHandle handle)
        {
            var h = handle as UnityAsyncHandle;

            return new UnityAsyncHandle(Addressables.UnloadSceneAsync(h.InternalHandle));
        }
    }
}
