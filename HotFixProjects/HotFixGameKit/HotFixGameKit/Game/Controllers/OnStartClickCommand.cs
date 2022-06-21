using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace HotFixGameKit.Game
{
    /// <summary>
    /// 开始游戏按钮逻辑
    /// </summary>
    public class OnStartClickCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            base.Execute(notification);

            //var gameDirector = notification.Body as GameDirector;
            //var facade = gameDirector.facade;
            var gameDirector = GameDirector.Instance;

            //转场
            var switchArgs = new SwitchSceneArgs();
            switchArgs.sceneName = "Main";
            switchArgs.complete += () => {
                Debug.Log("Main Start");
            };
            SendNotification(nameof(SwitchSceneCommand), switchArgs);
        }


    }
}
