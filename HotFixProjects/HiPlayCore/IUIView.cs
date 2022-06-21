using System;
using System.Collections.Generic;
using System.Text;

namespace HiPlayCore
{
    public interface IUIView<TTransform, TRectTransform> : IView<TTransform>
    {
        /// <summary>
        /// UI变换
        /// </summary>
        TRectTransform RectTransform { get; set; }

        /// <summary>
        /// 透明度
        /// </summary>
        float Alpha { get; set; }

        /// <summary>
        /// 是否可交互
        /// </summary>
        bool Interactable { get; set; }

        /// <summary>
        /// 进入动画
        /// </summary>
        IAnimation EnterAnimation { get; set; }

        /// <summary>
        /// 退出动画
        /// </summary>
        IAnimation ExitAnimation { get; set; }
    }
}

//+RectTransform RectTransform + float Alpha + bool Interactable + CanvasGroup CanvasGroup + IAnimation EnterAnimation + IAnimation ExitAnimation