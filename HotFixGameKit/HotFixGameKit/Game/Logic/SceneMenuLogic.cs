using System;
using UnityEngine;

namespace HotFixGameKit
{
    public class SceneMenuLogic : ISceneLogic
    {
        public bool Enter()
        {
            //Debug.Log("SceneMenuLogic");

            //初始化ui模块
            //GameDirector.Instance.facade.RegisterCommand(nameof(StartupMenuCommand), () => new StartupMenuCommand());
            //GameDirector.Instance.facade.SendNotification(nameof(StartupMenuCommand));


            return true;
        }

        public bool Exit()
        {
            //GameDirector.Instance.facade.RemoveCommand(nameof(StartupMenuCommand));
            return true;
        }
    }
}
