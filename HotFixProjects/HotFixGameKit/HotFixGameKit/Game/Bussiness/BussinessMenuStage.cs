using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace HotFixGameKit.Game
{
    public class BussinessMenuStage : BussinessBase
    {
        
        public override async void DoBussiness(params object[] args)
        {
            //切换场景
            var handle = StageManager.LoadSceneAsync("Menu");
            await handle.Task;
            InitMenuUI();
            OnBussinessEnd(this, new EventArgs());
        }

        void InitMenuUI()
        {

            //var parent = GameObject.Find("Canvas").transform;
            ////根据数据创建按钮
            //var handle = StageManager.CreateGameObjectAsync("MenuButtons", parent);
            //await handle.Task;
            //var go = handle.Result as GameObject;
            //var view = new ButtonView(go);
            //var startGame = BussinessManager.CreateBussiness("BussinessStartGame");
            //view.onClick += (sender,args) => { startGame.DoBussiness(); };
        }


    }
}
