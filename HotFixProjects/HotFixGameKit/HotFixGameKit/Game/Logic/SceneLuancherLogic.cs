using System;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace HotFixGameKit
{

    public class SceneLuancherLogic : ISceneLogic
    {
        public bool Enter()
        {
            //var mgr = GameDirector.Instance.sceneResourceManager;
            //mgr.LoadAsync("Menu", LoadSceneMode.Additive);
            //mgr.onSceneLoadComplete += OnSceneLoadComplete;
            //mgr.onSceneActive += OnSceneActive;
            return true;
        }

        private void OnSceneActive(object sender, EventArgs args)
        {
            //throw new NotImplementedException();
            //Debug.Log("OnSceneActive");
            Exit();

            //var logicMgr = GameDirector.Instance.sceneLogicMgr;
            //var logic = logicMgr.GetSceneLogic("Menu");
            //logic.Enter();
        }

        private void OnSceneLoadComplete(object sender, EventArgs args)
        {
            //throw new NotImplementedException();
        }

        public bool Exit()
        {
            //var mgr = GameDirector.Instance.sceneResourceManager;
            //mgr.onSceneLoadComplete -= OnSceneLoadComplete;
            //mgr.onSceneActive -= OnSceneActive;
            return true;
        }
    }
}
