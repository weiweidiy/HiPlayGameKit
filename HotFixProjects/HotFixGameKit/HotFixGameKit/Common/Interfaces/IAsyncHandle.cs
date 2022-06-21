using System;
using System.Threading.Tasks;

namespace HotFixGameKit
{
    public interface IAsyncHandle
    {
        object Result { get; }

        float PercentComplete { get; }

        bool IsDone { get; }

        Task<object> Task { get; }

    }
}
