using System;
using System.Collections.Generic;

namespace EventCore
{

    /// <summary>
    /// 事件管理器：
    /// 1-按添加监听的顺序分发事件
    /// 2-允许在事件处理器中拦截事件，阻止继续传递
    /// 3-允许强制处理事件（即使已经被拦截的事件，也能进行处理，比如log,trace等)
    /// 
    /// to do: 
    /// 1-增加传递顺序（冒泡，隧道，直通）
    /// 2-允许指定监听器顺序（如UI层级结构）
    /// </summary>
    public class EventManager
    {

        public delegate void EventDelegate<T>(T e) where T : Event;

        /// <summary>
        /// 事件类型和事件处理委托字典
        /// </summary>
        Dictionary<Type, List<HandlerWrapper>> delegates = new Dictionary<Type, List<HandlerWrapper>>();

        /// <summary>
        /// 用于查询委托
        /// </summary>
        Dictionary<Delegate, HandlerWrapper> lookup = new Dictionary<Delegate, HandlerWrapper>();

        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="del"></param>
        public void AddListener<T>(EventDelegate<T> del, bool forceHandle = false) where T : Event
        {
            if (lookup.ContainsKey(del))
                return;

            EventDelegate internalDel = (e) => del((T)e);
            var wrapper = new HandlerWrapper(internalDel, forceHandle);

            //加入查询字典
            lookup.Add(del, wrapper);

            List<HandlerWrapper> tempDel;
            var eType = typeof(T);
            if (delegates.TryGetValue(eType, out tempDel))
            {
                tempDel.Add(wrapper);
                delegates[eType] = tempDel;
            }
            else
            {
                delegates[eType] = new List<HandlerWrapper>() { wrapper };
            }
        }

        /// <summary>
        /// 移除事件监听器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="del"></param>
        public void RemoveListener<T>(EventDelegate<T> del) where T : Event
        {
            HandlerWrapper internalDelegate;
            if (lookup.TryGetValue(del, out internalDelegate))
            {
                List<HandlerWrapper> tempDel;
                var eType = typeof(T);
                if (delegates.TryGetValue(eType, out tempDel))
                {
                    //tempDel -= internalDelegate;
                    tempDel.Remove(internalDelegate);
                    if (tempDel.Count == 0)
                    {
                        delegates.Remove(eType);
                    }
                    else
                    {
                        delegates[eType] = tempDel;
                    }
                }

                lookup.Remove(del);
            }
        }

        /// <summary>
        /// 发出事件
        /// </summary>
        /// <param name="e"></param>
        public void Raise(Event e)
        {
            List<HandlerWrapper> delList;
            if (delegates.TryGetValue(e.GetType(), out delList))
            {
                for (int i = delList.Count - 1; i >= 0; i--)
                {
                    var d = delList[i];
                    if (!e.Handled || d.ForceHandle)
                    {
                        d.InvokeHandler(e);
                    }
                }
            }
        }

        /// <summary>
        /// 返回指定事件的处理器数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int GetCount<T>() where T : Event
        {
            List<HandlerWrapper> delList;
            var eType = typeof(T);
            if (delegates.TryGetValue(eType, out delList))
            {
                return delList.Count;
            }
            else
            {
                return 0;
            }
        }
    }

    /// <summary>
    /// 事件委托
    /// </summary>
    /// <param name="e"></param>
    public delegate void EventDelegate(Event e);
}