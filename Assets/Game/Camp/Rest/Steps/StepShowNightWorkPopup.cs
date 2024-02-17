using Assets.Game.HappeningSystem;
using Assets.Modules;
using Entities;
using ExtraInjection;
using Model.Entities.Answers;
using Model.Types;
using UnityEngine;
using Zenject;

namespace Assets.Game.Camp
{
    //1
    class StepShowNightWorkPopup : IRestStep, IExtraInject
    {
        private readonly PopupManager popupManager;
        private readonly NightWorkAdapter nightWorkAdapter;
        private readonly NightWorkModel nightWorkModel;
        private readonly RestContext restContext;
        private ICallBack callBack;
        public StepShowNightWorkPopup(PopupManager popupManager, NightWorkAdapter nightWorkAdapter,
            NightWorkModel nightWorkModel, RestContext restContext)
        {
            this.popupManager = popupManager;
            this.nightWorkAdapter = nightWorkAdapter;
            this.nightWorkModel = nightWorkModel;
            this.restContext = restContext;
        }
        void IRestStep.Execute(ICallBack callBack)
        {
            this.callBack = callBack;
            nightWorkModel.OnChosenNighWorkParam += Return;

            var popup = popupManager.ShowPopup(PopupType.NightWorkPopup);
            nightWorkAdapter.Init((NightWorkView)popup, nightWorkModel);
        }
        private void Return(ParameterType param)
        {
            nightWorkModel.OnChosenNighWorkParam -= Return;
            restContext.ChosenParam = param;
            popupManager.ClosePopup(PopupType.NightWorkPopup);
            callBack.Return(true);
        }

        
    }
}
