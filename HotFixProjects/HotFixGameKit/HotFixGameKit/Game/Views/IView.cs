using UnityEngine;

namespace HotFixGameKit.Game
{
    public interface IView
    {

        void Initialize(GameObject go);
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="data"></param>
        void Refresh(object data);
    }
}
