using UnityEngine;

namespace HotFixGameKit.Game
{
    public class GameViewManager
    {
        IReflectionInstantiator _instantiator;

        public GameViewManager(IReflectionInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        //public IView CreateView(string key)
        //{
        //    string viewFullName = "";
        //    switch (key)
        //    {
        //        case "MenuButtons":
        //            viewFullName = "HotFixGameKit.Game." + nameof(SimpleOneButtonView);
        //            break;
        //        default:
        //            Debug.LogError(key + "没有对应的View");
        //            break;
        //    }
        //    return _instantiator.Instantiate(viewFullName) as IView;
        //}
    }
}
