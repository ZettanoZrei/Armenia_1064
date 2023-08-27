using Assets.Game.Camp;
using Assets.Game.HappeningSystem.AfterHappenAction;
using Assets.Game.HappeningSystem.Happenings;
using Assets.Game.HappeningSystem.Persons;
using Assets.Game.Message;
using Assets.Modules;
using Model.Entities.Answers;
using Model.Entities.Happenings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class DialogManager : IHappeningManager
    {
        private readonly DialogModelDecorator dialogModelDecorator;
        private readonly HappeningModel model;
        private readonly MySceneManager sceneManager;
        private readonly CampIncomingData campIncomingData;
        private readonly AfterActionManager afterActionManager;
        private readonly MessageManager messageManager;
        private readonly SetupCampManager setupCampManager;
        private ICallBack launcherCallBack;
        private Scene currentScene;
        public DialogManager(DialogModelDecorator dialogModelDecorator, HappeningModel model, MySceneManager sceneManager, CampIncomingData campIncomingData,
            AfterActionManager afterActionManager, MessageManager messageManager, SetupCampManager setupCampManager)
        {
            this.dialogModelDecorator = dialogModelDecorator;
            this.model = model;
            this.sceneManager = sceneManager;
            this.campIncomingData = campIncomingData;
            this.afterActionManager = afterActionManager;
            this.messageManager = messageManager;
            this.setupCampManager = setupCampManager;
        }

        void IHappeningManager.Init(Happening data, ICallBack callBack)
        {
            this.launcherCallBack = callBack;
            model.InitHappeningData(data);
            dialogModelDecorator.InitModel(model);
            Subcribe();
        }

        void IHappeningManager.Perform()
        {
            this.currentScene = sceneManager.CurrentScene;
            this.sceneManager.LoadDialogScene();
        }
        private void StartFinishHappenig(HappeningModel model, AfterAction afterAction)
        {
            Action<Scene> middleAction = (_) => MiddleFinishHappening(afterAction);
            middleAction += (_) => sceneManager.OnChangeScene_Post -= middleAction;
            sceneManager.OnChangeScene_Post += middleAction;
            LoadSceneBack(model);
        }

        private async void MiddleFinishHappening(AfterAction afterAction)
        {
            await Task.Delay(50);
            if (IsResultMessage(afterAction))
            {
                Action action = () => EndFinishHappening(afterAction);
                action += () => messageManager.OnFinishMessage -= action;
                messageManager.OnFinishMessage += action;               
                messageManager.Perform(afterAction.Consequences);
            }
            else
            {
                EndFinishHappening(afterAction);
            }
        }

        private void EndFinishHappening(AfterAction afterAction)
        {
            Logger.WriteLog($"FinishHappenig: квест {model.HappeningData.Quest}, событие: {model.Title}");
            this.launcherCallBack.Return(model.HappeningData);
            Unsubscribe();

            afterActionManager.SetAction(afterAction);
            afterActionManager.Do();
        }

        private bool IsResultMessage(AfterAction afterAction)
        {
            //this is костыль to not show zero value in message conclution window
            if (afterAction.Consequences == null)
                return false;

            afterAction.Consequences.PersonConsequences = afterAction.Consequences.PersonConsequences.Where(x => x.Value != 0).ToList();
            afterAction.Consequences.ParamConsequences = afterAction.Consequences.ParamConsequences.Where(x => x.Value != 0).ToList();
            var isPersonConsequences = afterAction.Consequences.IsPersonCons && afterAction.Consequences.PersonConsequences.Any();
            var isParamConsequences = afterAction.Consequences.IsParamCons && afterAction.Consequences.ParamConsequences.Any();


            return afterAction.Consequences != null
                && (afterAction.Consequences.IsMessage || isParamConsequences || isPersonConsequences);

        }

        private void Subcribe()
        {
            dialogModelDecorator.OnFinishHappeningModel += StartFinishHappenig;

        }
        private void Unsubscribe()
        {
            dialogModelDecorator.OnFinishHappeningModel -= StartFinishHappenig;
        }

        private void LoadSceneBack(HappeningModel _)
        {
            if (currentScene is Scene.CampScene)
            {
                setupCampManager.SetupCamp(campIncomingData.DialogAvailable);
            }
            else if (sceneManager.IsTravelScene(currentScene))
            {
                sceneManager.LoadScene(currentScene);
            }
        }



        public class Factory : PlaceholderFactory<DialogManager>
        {
        }
    }
}
