using Assets.Game.HappeningSystem.AfterHappenAction;
using Assets.Game.HappeningSystem.Happenings;
using Assets.Game.HappeningSystem.Persons;
using Assets.Game.Message;
using Assets.Modules;
using Entities;
using ExtraInjection;
using Model.Entities.Answers;
using Model.Entities.Happenings;
using System;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class AccidentManager : IHappeningManager
    {
        private readonly HappeningModel model;
        private readonly AccidentPresenter accidentPresenter;
        private readonly AfterActionManager afterActionManager;
        private readonly MessageManager messageManager;
        private PopupManager popupManager;
        private ICallBack launcherCallBack;
        public AccidentManager(HappeningModel model, AccidentPresenter accidentPresenter, AfterActionManager afterActionManager, 
            MessageManager messageManager, PopupManager popupManager)
        {
            this.model = model;
            this.accidentPresenter = accidentPresenter;
            this.afterActionManager = afterActionManager;
            this.messageManager = messageManager;
            this.popupManager = popupManager;
        }

        void IHappeningManager.Init(Happening data, ICallBack callBack)
        {
            this.launcherCallBack = callBack;
            model.InitHappeningData(data);
            var accidentView = popupManager.ShowPopup(PopupType.AccidentPopup) as AccidentView;
            accidentPresenter.Init(model, accidentView);
            Subcribe();
        }

        private void StartFinishHappenig(HappeningModel model, AfterAction afterAction)
        {
            popupManager.ClosePopup(PopupType.AccidentPopup);
            if (IsResultMessage(afterAction))
            {
                Action action = () => EndFinishHappening(afterAction);
                action += () => messageManager.OnFinishMessage -= action;
                messageManager.OnFinishMessage += action;
                //await Task.Delay(200);
                messageManager.Perform(afterAction.Consequences);
            }
            else
            {
                EndFinishHappening(afterAction);
            }
        }

        private void EndFinishHappening(AfterAction afterAction)
        {
            Logger.WriteLog($"FinishHappenig: квест - {model.HappeningData.Quest}, событие - {model.Title}");
            launcherCallBack.Return(model.HappeningData);
            Unsubcribe(model);

            afterActionManager.SetAction(afterAction);
            afterActionManager.Do();
        }

        private bool IsResultMessage(AfterAction afterAction)
        {
            return afterAction.Consequences != null
                && (afterAction.Consequences.IsMessage || afterAction.Consequences.IsParamCons || afterAction.Consequences.IsPersonCons);

        }
        private void Unsubcribe(HappeningModel model)
        {
            model.OnFinishHappeningModel -= StartFinishHappenig;
        }
        private void Subcribe()
        {
            model.OnFinishHappeningModel += StartFinishHappenig;
        }
        void IHappeningManager.Perform()
        {
            accidentPresenter.Begin();
        }

        public class Factory : PlaceholderFactory<AccidentManager>
        {
        }
    }
}
