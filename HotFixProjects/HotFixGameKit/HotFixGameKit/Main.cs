using HotFixGameKit.Game;
using UnityEngine;
using HotFixModuleTest;

namespace HotFixGameKit
{

    public class Main
    {
        public void Startup()
        {
            Debug.Log("IL Startup");

            var module = new TestModule();
            module.Show(" main arg 1");

            //GameDirector.Instance.Launch();
        }

    }
}
