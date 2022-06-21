using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace HotFixGameKit.Game
{
    public class SceneEventArgs : EventArgs
    {
        SceneInstance scene;
        public SceneEventArgs(SceneInstance scene):base()
        {
            this.scene = scene;
        }
    }

    /// <summary>
    /// 切换场景
    /// </summary>
    public class SceneManager
    {
        //to do: 增加卸载完成
        public event EventHandler onSceneLoadComplete;
        public event EventHandler onSceneActive;

        /// <summary>
        /// 场景加载器
        /// </summary>
        ISceneLoaderAsync _sceneLoader;

        Dictionary<string, IAsyncHandle> _dicHandles = new Dictionary<string, IAsyncHandle>();

        private SceneManager() { }
        public SceneManager(ISceneLoaderAsync sceneLoader)
        {
            this._sceneLoader = sceneLoader;
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="loadSceneMode"></param>
        /// <param name="activeOnLoad"></param>
        public async void LoadAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single, bool activeOnLoad = true)
        {
            if(GetHandle(sceneName) != null)
            {
                throw new InvalidKeyException("场景" + sceneName + "已经存在，无需重复加载！");
            }

            var handle = _sceneLoader.LoadSceneAsync(sceneName, loadSceneMode, activeOnLoad);

            AddHandle(sceneName, handle);

            await handle.Task;

            var scene = ((SceneInstance)(handle.Result));
 
            onSceneLoadComplete?.Invoke(this, new SceneEventArgs(scene));

            if (activeOnLoad)
                onSceneActive?.Invoke(this, new SceneEventArgs(scene));
        }

        /// <summary>
        /// 异步卸载场景
        /// </summary>
        /// <param name="sceneName"></param>
        public async void UnLoadAsync(string sceneName)
        {
            var handle = GetHandle(sceneName);
            if (handle != null)
            {
                var unloadHandle = _sceneLoader.UnLoadSceneAsync(handle);
                await unloadHandle.Task;
                RemoveHandle(sceneName);
            }
            else
            {
                throw new InvalidKeyException("找不到需要移除的场景 " + sceneName);
            }
        }

        /// <summary>
        /// 缓存异步handle到一个字典中
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="handle"></param>
        void AddHandle(string sceneName , IAsyncHandle handle)
        {
            _dicHandles.Add(sceneName, handle);
        }

        /// <summary>
        /// 移除handle缓存
        /// </summary>
        /// <param name="sceneName"></param>
        void RemoveHandle(string sceneName)
        {
            _dicHandles.Remove(sceneName);
        }

        /// <summary>
        /// 获取异步handle
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        IAsyncHandle GetHandle(string sceneName)
        {
            if (_dicHandles.ContainsKey(sceneName))
                return _dicHandles[sceneName];
            else
                return null;
        }

        /// <summary>
        /// 激活一个场景，必须要加载完成
        /// </summary>
        /// <param name="sceneName"></param>
        public void ActiveAsync(string sceneName)
        {
            var handle = GetHandle(sceneName);
            if (handle == null)
                throw new InvalidKeyException("场景" + sceneName + "没有被加载，需要先调用LoadAsync方法！");


            if(handle.IsDone)
            {
                var scene = ((SceneInstance)(handle.Result));

                scene.ActivateAsync().completed += (op) =>
                {
                    onSceneActive?.Invoke(this, new SceneEventArgs(scene));
                };
            }
            else
            {
                throw new InvalidOperationException("场景" + sceneName + "还没有加载完成，请在onSceneLoadComplete后调用！");
            }
        }


        //public EventHandler OnLoadStart;
        //public EventHandler OnLoadComplete;
        //public EventHandler OnActive;

        ///// <summary>
        ///// 加载进度
        ///// </summary>
        //public float PercentProgress
        //{
        //    get
        //    {

        //        return _handle.PercentComplete;
        //    }
        //    private set
        //    {

        //    }
        //}

        ///// <summary>
        ///// 是否正在加载
        ///// </summary>
        //public bool IsLoading { get; set; }

        ///// <summary>
        ///// 是否加载完成
        ///// </summary>
        //public bool IsLoadingComplete { get; set; }

        ///// <summary>
        ///// 加载完成后，是否立即激活
        ///// </summary>
        //bool _activeOnLoad;

        ///// <summary>
        ///// 场景加载器
        ///// </summary>
        //ISceneLoaderAsync _sceneLoader;

        ///// <summary>
        ///// 异步加载handle
        ///// </summary>
        //IAsyncHandler _handle;

        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="sceneLoader"></param>
        //public SceneManager(ISceneLoaderAsync sceneLoader)
        //{
        //    _sceneLoader = sceneLoader;
        //}

        ///// <summary>
        ///// 异步加载一个场景
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="loadSceneMode"></param>
        ///// <param name="activeOnLoad"></param>
        //public IAsyncHandler LoadSceneAsync(string key, LoadSceneMode loadSceneMode = LoadSceneMode.Single, bool activeOnLoad = true)
        //{
        //    if (IsLoading)
        //    {
        //        throw new Exception("有场景正在加载中, 请调用 IsLoading判断");
        //    }

        //    _activeOnLoad = activeOnLoad;

        //    _handle = _sceneLoader.LoadSceneAsync(key, loadSceneMode, activeOnLoad);

        //    _handle.Completed += LoadCompleted;

        //    IsLoading = true;

        //    OnLoadStart?.Invoke(this, new EventArgs());

        //    return _handle;
        //}

        ///// <summary>
        ///// 加载完成回调
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="obj"></param>
        //private void LoadCompleted(object sender, AsyncOperationHandle obj)
        //{
        //    IsLoading = false;

        //    IsLoadingComplete = true;

        //    OnLoadComplete?.Invoke(this, new EventArgs());

        //    if (_activeOnLoad)
        //    {
        //        OnActive?.Invoke(this, new EventArgs());
        //        Clear();
        //    }
        //}

        ///// <summary>
        ///// 手动激活场景
        ///// </summary>
        //public void ActiveSceneAsync()
        //{
        //    if (!IsLoadingComplete)
        //    {
        //        Debug.LogError("场景还没有加载完, 请调用 IsLoadingComplete 判断");
        //        return;
        //    }

        //    ((SceneInstance)_handle.Result).ActivateAsync().completed += ActiveComplete;
        //}

        ///// <summary>
        ///// 场景激活完成回调
        ///// </summary>
        ///// <param name="obj"></param>
        //private void ActiveComplete(AsyncOperation obj)
        //{
        //    OnActive?.Invoke(this, new EventArgs());
        //    Clear();
        //}

        ///// <summary>
        ///// 清理场景
        ///// </summary>
        //private void Clear()
        //{
        //    IsLoading = false;
        //    IsLoadingComplete = false;
        //    PercentProgress = -1;
        //    _sceneLoader.Release(_handle);
        //}
    }
}
