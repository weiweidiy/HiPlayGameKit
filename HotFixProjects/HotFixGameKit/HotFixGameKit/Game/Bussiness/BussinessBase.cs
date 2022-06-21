using System;
using System.Collections.Generic;

namespace HotFixGameKit.Game
{

    public abstract class BussinessBase : IBussiness
    {
        public event EventHandler onBussinessEnd;

        public BussinessManager BussinessManager { get; set; }
        public StageManager StageManager { get; set; }

        
        protected void OnBussinessEnd(object sender, EventArgs args)
        {
            onBussinessEnd?.Invoke(sender, args);
        }


        public abstract void DoBussiness(params object[] args);
    }
}
