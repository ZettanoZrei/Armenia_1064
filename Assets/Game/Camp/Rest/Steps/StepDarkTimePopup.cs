using Assets.Game.Configurations;
using Assets.Modules;

namespace Assets.Game.Camp
{
    //3
    public class StepDarkTimePopup : IRestStep, ICallBack
    {
        private readonly PopupManager popupManager;
        private readonly PopupConfig.BlackPanelConfig popupConfig;
        private BlackPanelPopup popup;
        private ICallBack callBack;
        public StepDarkTimePopup(PopupManager popupManager, PopupConfig popupConfig)
        {
            this.popupManager = popupManager;
            this.popupConfig = popupConfig.blackPanelConfig;
        }
        void IRestStep.Execute(ICallBack callBack)
        {
            popup = popupManager.ShowPopup(PopupType.BlackPanelPopup) as BlackPanelPopup;           
            popup.OnFinish += Return;
            popup.Show(popupConfig.darkUpSpeed, popupConfig.darkDownSpeed, popupConfig.delayAfterDark);
            this.callBack = callBack;
        }

        public void Return(object _)
        {
            popup.OnFinish -= Return;
            popupManager.ClosePopup(PopupType.BlackPanelPopup);
            callBack.Return(true);
        }
    }
}
