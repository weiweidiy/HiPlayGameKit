using System.Threading.Tasks;
using UnityEngine;

namespace HotFixGameKit.Game
{
    public class UIManager
    {
        IGameObjectLoaderAsync instantiator = null;

        public UIManager(IGameObjectLoaderAsync instantiator)
        {
            this.instantiator = instantiator;
        }

        public Task<object> Open(string key, Transform parent)
        {
            var handle = instantiator.InstantiatorAsync(key, parent);
            return handle.Task;
        }

        public Task<object> Open(string key)
        {
            return Open(key, null);
        }

        public async Task<TView> Open<TView>(string key, Transform parent) where TView : IView, new()
        {
            var go = await Open(key, parent);
            var view = new TView();
            //view.Bind(go as GameObject);
            return view;
        }

        public void Close(string key)
        {

        }
    }
}
