using HotFixGameKit.Game;
using UnityEngine;
using HotFixModuleTest;
using HiPlayCore;
using System;

namespace HotFixGameKit
{

    public class Main
    {
        public void Startup()
        {
            Debug.Log("IL Startup");

            var module = new TestModule();
            module.Show(" main arg 1");

            GameDirector.Instance.Launch();

            GameObject.Find("Luancher").GetComponent<Luancher>().ILManager.LoadAssembly("HiPlayCore");

            //var test = new CoreTest();
            //Debug.Log("CoreTest Value = " + test.Value);

        }

    }
}
