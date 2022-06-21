using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableDownLoadMgr
{
    public event EventHandler onInitComplete;
    public event EventHandler onDownloadStart;
    public event EventHandler onDownloadEnd;

    /// <summary>
    /// 下载handle
    /// </summary>
    AsyncOperationHandle _downloadHandle;

    /// <summary>
    /// 下载进度
    /// </summary>
    public float PercentProgress
    {
        get
        {
            if (_downloadHandle.IsValid())
            {
                return _downloadHandle.PercentComplete;
            }
            return 0;
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public async void Initialize()
    {
        var handle = Addressables.InitializeAsync();
        var task = handle.Task;
        await task;
        onInitComplete?.Invoke(this, new EventArgs());
        //Addressables.InitializeAsync().Completed += (obj) =>
        //{
        //    onInitComplete?.Invoke(this, new EventArgs());
        //};
    }

    /// <summary>
    /// 检查下载大小
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<long> CheckDownloadSize(string key)
    {
        var handle = Addressables.GetDownloadSizeAsync(key);
        var task = handle.Task;

        long getDownloadSize = await task;
        Addressables.Release(handle);

        return getDownloadSize;
    }



    /// <summary>
    /// 开始异步下载
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<object> StartDownloadAsync(string key)
    {
        _downloadHandle = Addressables.DownloadDependenciesAsync(key);

        var task = _downloadHandle.Task;


        onDownloadStart?.Invoke(this, new EventArgs());

        var obj = await task;

        onDownloadEnd?.Invoke(this, new EventArgs());

        Addressables.Release(_downloadHandle);

        return obj;
    }



}
