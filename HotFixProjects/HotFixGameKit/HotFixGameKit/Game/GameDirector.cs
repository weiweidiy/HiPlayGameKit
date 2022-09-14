using PureMVC.Patterns.Facade;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HotFixGameKit.Game
{
    public class GameDirector
    {
        static GameDirector _instance = null;

        public static GameDirector Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameDirector();
                    _instance.Initialize();
                }

                return _instance;
            }
        }

        //public SceneLogicManager sceneLogicMgr = null;

        //public SceneManager sceneResourceManager = null;

        //public UIManager uiManager = null;

        public AppFacade facade = null;

        public StageManager stageManager = null;

        //BussinessManager bussinessManager = null;

        public ISceneLoaderAsync sceneLoader = new UnitySceneLoaderAddressables();

        public GameObjectManager gameObjectManager;



        void Initialize()
        {
            facade = new AppFacade("Game");

            //stageManager = new StageManager(new UnitySceneLoaderAddressables()
            //                               ,new AssetLoaderAddressables());

            //bussinessManager = new BussinessManager(stageManager);

            gameObjectManager = new GameObjectManager(new AssetLoaderAddressables());
        }

        public void Launch()
        {
            //var launch = bussinessManager.CreateBussiness("BussinessLaunch");
            //launch.DoBussiness();

            var launcher = new MVCLauncher(this);
            launcher.Launch();


        }

    }


    public class AppFacade : Facade
    {
        public AppFacade(string key) : base(key)
        {

        }

        protected override void InitializeController()
        {
            base.InitializeController();

            RegisterCommand(nameof(StartupMenuCommand), () => new StartupMenuCommand());
        }
    }
}
