using PureMVC.Patterns.Facade;
using System;
using UnityEngine;

namespace HotFixGameKit.Game
{
    /// <summary>
    /// MVC方式启动游戏
    /// </summary>
    public class MVCLauncher : ILauncher
    {
        GameDirector _gameDirector;
        public MVCLauncher(GameDirector gameDirector)
        {
            _gameDirector = gameDirector;
        }
        private MVCLauncher() { }

        public void Launch()
        {
            var facade = _gameDirector.facade;
            var switchArgs = GetSwitchArgs();

            facade.RegisterCommand(nameof(SwitchSceneCommand), () => new SwitchSceneCommand());
            facade.SendNotification(nameof(SwitchSceneCommand), switchArgs);
        }

        SwitchSceneArgs GetSwitchArgs()
        {
            var facade = _gameDirector.facade;
            var sceneLoader = _gameDirector.sceneLoader;

            var switchArgs = new SwitchSceneArgs();
            switchArgs.sceneName = "Menu";
            switchArgs.complete += () =>
            {
                facade.SendNotification(nameof(StartupMenuCommand));
            };
            return switchArgs;
        }
    }
}
