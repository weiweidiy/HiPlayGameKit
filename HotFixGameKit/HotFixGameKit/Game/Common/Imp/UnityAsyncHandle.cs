using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace HotFixGameKit.Game
{
    public class ObjectEventArgs : EventArgs
    {
        public object Result { get; private set; }
        public ObjectEventArgs(object obj):base()
        {
            Result = obj;
        }
    }
    public class UnityAsyncHandle : IAsyncHandle
    {
        public event EventHandler Complete;

        public AsyncOperationHandle InternalHandle { get; private set; }

        public UnityAsyncHandle(AsyncOperationHandle internalHandle)
        {
            InternalHandle = internalHandle;
            internalHandle.Completed += (obj) =>
            {
                Complete?.Invoke(this, new ObjectEventArgs(obj.Result));
            };
        }

        public object Result => InternalHandle.Result;

        public float PercentComplete => InternalHandle.PercentComplete;

        public Task<object> Task => InternalHandle.Task;

        public bool IsDone => InternalHandle.IsDone;

        
    }
}
