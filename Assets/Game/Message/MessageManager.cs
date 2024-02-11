using Assets.Game.HappeningSystem;
using Assets.Modules;
using Entities;
using ExtraInjection;
using Model.Entities.Answers;
using System;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Game.Message
{
    public class MessageManager : ICallBack, IExtraInject
    {
        [ExtraInject] private PopupManager popupManager;
        private IPopup popup;
        private MessageAdapter messageAdapter;
        public event Action OnFinishMessage;

        public void Perform(Consequences consequences)
        {
            popup = popupManager.ShowPopup(PopupType.MessagePopup);
            messageAdapter = new MessageAdapter(popup, consequences, this);
            messageAdapter.Perform();
        }

        public void Return(object _)
        {
            popupManager.ClosePopup(popup.PopupType);
            OnFinishMessage.Invoke();
        }
    }
}
