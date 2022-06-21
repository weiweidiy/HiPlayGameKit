using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HotFixGameKit
{

    public class SceneLogicManager
    {
        IReflectionInstantiator instantiator;

        Dictionary<string, ISceneLogic> dicSceneLogic = new Dictionary<string, ISceneLogic>();
        public SceneLogicManager(IReflectionInstantiator instantiator)
        {
            this.instantiator = instantiator;
        }

        ISceneLogic GetSceneLogicFromDictionary(string sceneName)
        {
            if (dicSceneLogic.ContainsKey(sceneName))
                return dicSceneLogic[sceneName];

            return null;
        }

        ISceneLogic CreateSceneLogic(string sceneName)
        {
            string classFullName = "HotFixGameKit.Scene" + sceneName + "Logic";
            return (ISceneLogic)instantiator.Instantiate(classFullName);
        }

        public ISceneLogic GetSceneLogic(string sceneName)
        {

            ISceneLogic sceneLogic = GetSceneLogicFromDictionary(sceneName);
            
            if(sceneLogic == null)
            {
                sceneLogic = CreateSceneLogic(sceneName);
                dicSceneLogic.Add(sceneName, sceneLogic);
            }

            return sceneLogic;
        }
    }
}
