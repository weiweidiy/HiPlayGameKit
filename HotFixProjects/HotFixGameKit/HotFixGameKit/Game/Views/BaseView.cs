using UnityEngine;

namespace HotFixGameKit.Game
{
    public abstract class BaseView : IView
    {
        GameObject _go;

        object _data;

        public virtual void Refresh(object data) { _data = data; }

        public virtual void Initialize(GameObject go){ _go = go; }
    }
}
