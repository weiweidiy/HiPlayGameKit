using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace HotFixGameKit.Game
{
    public class DownloadAddressable : SimpleCommand
    {
        public async override void Execute(INotification notification)
        {
            var gameDirector = GameDirector.Instance;
            var facade = gameDirector.facade;

            if(CheckDownload())
            {

            }
            else
            {

            }
            
        }

        bool CheckDownload()
        {
            return true;
        }
    }
    /// <summary>
    /// 初始化Menu场景
    /// </summary>
    public class StartupMenuCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            base.Execute(notification);

 
            CreateMenuButton();
        }

        /// <summary>
        /// 创建菜单按钮
        /// </summary>
        async void CreateMenuButton()
        {
            var gameDirector = GameDirector.Instance;
            var facade = gameDirector.facade;
            var canvas = GameObject.Find("Canvas");

            var handle = gameDirector.gameObjectManager.CreateGameObjectAsync("MenuButtons", canvas.transform);
            await handle.Task;

            var go = handle.Result as GameObject;
            var view = new SimpleOneButtonView();
            view.Initialize(go);

            //注册command
            facade.RegisterCommand(nameof(OnStartClickCommand), () => new OnStartClickCommand());
            //注册mediator
            facade.RegisterMediator(new MenuMediator(nameof(MenuMediator), view));
        }


    }
}
