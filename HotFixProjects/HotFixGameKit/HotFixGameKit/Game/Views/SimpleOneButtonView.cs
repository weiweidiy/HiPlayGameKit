using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HotFixGameKit.Game
{
    /// <summary>
    /// 简单的单个按钮视图
    /// </summary>
    public class SimpleOneButtonView : BaseView
    {
        public event EventHandler onClick;

        Button cmpButton;

        public override void Initialize(GameObject go)
        {
            base.Initialize(go);

            cmpButton = go.GetComponent<Button>();
            cmpButton.onClick.AddListener(OnButtonClick);
        }
        public override void Refresh(object data)
        {

        }
        private void OnButtonClick()
        {
            onClick?.Invoke(this, new EventArgs());
        }

 

 
    }
}
