using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace HotFixGameKit.Game
{


    public enum TransitionType
    {
        /// <summary>
        /// loading 场景
        /// </summary>
        LoadingScene,

    }

    /// <summary>
    /// 转场参数
    /// </summary>
    public class SwitchSceneArgs
    {
        public string sceneName;
        public Action complete;
    }

    /// <summary>
    /// 转场命令
    /// </summary>
    public class SwitchSceneCommand : SimpleCommand
    {
        public async override void Execute(INotification notification)
        {
            base.Execute(notification);

            var switchSceneArgs = notification.Body as SwitchSceneArgs;
            var gameDirector = GameDirector.Instance;
            var sceneLoader = gameDirector.sceneLoader;
            var facade = gameDirector.facade;
            //FadeOut

            //Load
            var handle = sceneLoader.LoadSceneAsync(switchSceneArgs.sceneName);
            await handle.Task;

            //加载完成
            switchSceneArgs.complete?.Invoke();

            //FadeIn
        }
    }
}
