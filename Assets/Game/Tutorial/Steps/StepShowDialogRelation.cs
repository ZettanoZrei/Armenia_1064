using Assets.Game.Core;
using Assets.Game.HappeningSystem;
using Assets.Game.HappeningSystem.Happenings;
using Assets.Game.Tutorial.Core;
using Assets.Game.Tutorial.UI;
using Assets.GameEngine;
using Entities;
using Model.Entities.Happenings;
using Model.Entities.Phrases;
using System;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Game.Tutorial.Steps
{
    //суть в то что сначала мы отслеживаем диалог,  а затем конец нода 
    class StepShowDialogRelation : StepShowPopup
    {
        private readonly MySceneManager sceneManager;
        private readonly DialogModelDecorator dialogModelDecorator;


        public override event Action<INarrativeStep<TutorialStepType>> OnLaunchStep;
        public StepShowDialogRelation(MySceneManager sceneManager, DialogModelDecorator dialogModelDecorator, 
            SignalBus signalBus, PopupType popupType) : base(popupType, signalBus)
        {
            this.stepType = TutorialStepType.DialogRelation;
            this.sceneManager = sceneManager;
            this.dialogModelDecorator = dialogModelDecorator;
        }

        public override void Begin()
        {
            sceneManager.OnChangeScene_Post += CheckIfDialog;
        }

        private void CheckIfDialog(Scene scene)
        {
            if (scene == Scene.DialogScene)
            {
                DoBegin1();
            }
        }

        private void DoBegin1()
        {
            sceneManager.OnChangeScene_Post -= CheckIfDialog;
            dialogModelDecorator.OnShowAnswers += DoBegin2;
        }

        private void DoBegin2()
        {
            dialogModelDecorator.OnShowAnswers -= DoBegin2;
            OnLaunchStep?.Invoke(this);
            popup = popupManager.ShowPopup(popupType) as TutorialPopup;
            popup.OnFinish += Finish;
        }

        public override void FinishScene()
        {
            sceneManager.OnChangeScene_Post -= CheckIfDialog;
            if (dialogModelDecorator.IsModel)
                dialogModelDecorator.OnShowAnswers -= DoBegin2;
        }
    }
}
