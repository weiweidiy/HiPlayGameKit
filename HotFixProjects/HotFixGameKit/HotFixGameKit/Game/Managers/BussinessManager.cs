using System.Collections.Generic;
using UnityEngine;

namespace HotFixGameKit.Game
{
    public class BussinessManager
    {
        StageManager _stageManager;

        HashSet<IBussiness> _hashSet = new HashSet<IBussiness>();

        public BussinessManager(StageManager stageManager)
        {
            _stageManager = stageManager;
        }

        public IBussiness CreateBussiness(string key)
        {
            ReflectionInstantiator helper = new ReflectionInstantiator();
            var result = helper.Instantiate("HotFixGameKit.Game." + key);
            var bussiness = result as BussinessBase;
            bussiness.onBussinessEnd += (sender, arg) => {
                _hashSet.Remove(bussiness);
            };
            bussiness.BussinessManager = this;
            bussiness.StageManager = _stageManager;

            _hashSet.Add(bussiness);

            CheckBussiness();
            return bussiness;
        }

        void CheckBussiness()
        {
            if (_hashSet.Count > 2)
            {
                string warning = "";
                foreach (var item in _hashSet)
                {
                    warning += item.GetType().ToString() + ",";
                }
                Debug.Log("同时运行的业务逻辑过多,完成的业务需要调用OnBussinessEnd " + warning);
            }
        }

       

    }
}
