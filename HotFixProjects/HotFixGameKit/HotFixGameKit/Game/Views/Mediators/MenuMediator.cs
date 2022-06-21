using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

namespace HotFixGameKit.Game
{
    public class MenuMediator : Mediator
    {
        public MenuMediator(string mediatorName, object viewComponent = null) : base(mediatorName, viewComponent)
        {

        }

        public override void HandleNotification(INotification notification)
        {
            base.HandleNotification(notification);
        }

        public override string[] ListNotificationInterests()
        {
            return base.ListNotificationInterests();
        }

        public override void OnRegister()
        {
            base.OnRegister();

            var simpleOneButtonView = ViewComponent as SimpleOneButtonView;
            simpleOneButtonView.onClick += Start_onClick;
        }

        private void Start_onClick(object sender, System.EventArgs e)
        {
            SendNotification(nameof(OnStartClickCommand));
        }

        public override void OnRemove()
        {
            base.OnRemove();
        }
    }
}
