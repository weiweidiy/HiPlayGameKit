using HotFixGameKit.Game;
using System;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace HotFixGameKit
{

    public class Main
    {
        public void Startup()
        {
            Debug.Log("IL Startup");


            //var logicMgr = GameDirector.Instance.sceneLogicMgr;
            //var logic = logicMgr.GetSceneLogic("Luancher");
            //logic.Enter();
            GameDirector.Instance.Launch();
        }

    }
}
