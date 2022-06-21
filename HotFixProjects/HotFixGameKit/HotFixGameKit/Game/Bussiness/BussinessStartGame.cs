using System.Collections.Generic;
using UnityEngine;

namespace HotFixGameKit.Game
{
    public class BussinessStartGame : BussinessBase
    {
        public override void DoBussiness(params object[] args)
        {
            var bussiness = BussinessManager.CreateBussiness("BussinessMainStage");
            bussiness.DoBussiness();
        }
    }
}
