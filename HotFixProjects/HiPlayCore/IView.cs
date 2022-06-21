using System;
using System.Collections.Generic;
using System.Text;

namespace HiPlayCore
{
    public interface IView<T>
    {
        /// <summary>
        /// 视图名字
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        bool Visibility { get; set; }

        /// <summary>
        /// 拥有该实例的对象
        /// </summary>
        T Owner { get; set; }

        /// <summary>
        /// 父对象
        /// </summary>
        T Parent { get; set; }

        /// <summary>
        /// 视图对象变幻：坐标，缩放等
        /// </summary>
        T Transform { get; set; }
    }
}

//+string Name + Transform Parent + GameObject Owner + Transform Transform + bool Visibility + IAttributes ExtraAttributes