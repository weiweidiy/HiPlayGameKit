using System;
using System.Collections.Generic;
using System.Text;

namespace HiPlayCore
{
    public interface IAnimation
    {
        /// <summary>
        /// 注册动画开始回调
        /// </summary>
        /// <param name="onStart"></param>
        /// <returns></returns>
        IAnimation OnStart(Action onStart);

        /// <summary>
        /// 注册动画结束回调
        /// </summary>
        /// <param name="onEnd"></param>
        /// <returns></returns>
        IAnimation OnEnd(Action onEnd);

        /// <summary>
        /// 播放
        /// </summary>
        /// <returns></returns>
        IAnimation Play();
    }
}
