using System;

namespace HotFixGameKit.Game
{
    public interface IBussiness
    {
        event EventHandler onBussinessEnd;
        void DoBussiness(params object[] args);
    }
}
