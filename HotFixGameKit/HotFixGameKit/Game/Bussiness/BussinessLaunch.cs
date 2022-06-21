using System;
using System.Collections.Generic;

namespace HotFixGameKit.Game
{
    public class BussinessLaunch : BussinessBase
    {
        public override void DoBussiness(params object[] args)
        {
            //var bussiness = _bussinessManager.GetMenuStage();
            var bussiness = BussinessManager.CreateBussiness("BussinessMenuStage");
            bussiness.DoBussiness();
            OnBussinessEnd(this,new EventArgs());
        }
    }
}
