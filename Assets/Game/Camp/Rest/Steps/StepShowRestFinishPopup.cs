using Assets.Game.HappeningSystem;
using Assets.Game.Message;
using Assets.Modules;
using Entities;
using Model.Entities.Answers;
using Zenject;

namespace Assets.Game.Camp
{
    //4
    public class StepShowRestFinishPopup : IRestStep, ICallBack
    {
        private readonly PopupManager popupManager;
        private readonly RestContext restContext;
        private ICallBack callBack;
        public StepShowRestFinishPopup(PopupManager popupManager, RestContext restContext)
        {
            this.popupManager = popupManager;
            this.restContext = restContext;
        }

        void IRestStep.Execute(ICallBack callBack)
        {
            this.callBack = callBack;
            var messagePopup = popupManager.ShowPopup(PopupType.RestFinishPopup);

            var messageAdapter = new MessageAdapter(messagePopup, restContext.RestConsequences, this);
            messageAdapter.Perform();
        }

        void ICallBack.Return(object _)
        {
            popupManager.ClosePopup(PopupType.RestFinishPopup);
            callBack.Return(true);
        }
    }
}
