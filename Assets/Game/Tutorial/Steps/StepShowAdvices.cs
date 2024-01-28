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
    class StepShowAdvices : StepShowPopup
    {
        private readonly HappeningManager happeningManager;
        public override event Action<INarrativeStep<TutorialStepType>> OnLaunchStep;
        protected string expectedHappening;
        private readonly DialogModelDecorator dialogModelDecorator;

        public StepShowAdvices(PopupManager popupManager, PopupType popupType, HappeningManager happeningManager,
            DialogModelDecorator dialogModelDecorator, SignalBus signalBus) : base(popupManager, popupType, signalBus)
        {
            this.happeningManager = happeningManager;
            this.stepType = TutorialStepType.Advices;
            this.expectedHappening = "Эпизод_1.2";
            this.dialogModelDecorator = dialogModelDecorator;
        }

        public override void Begin()
        {
            happeningManager.OnLaunchHappening += CheckCampIntroduceHappening;
        }

        private void CheckCampIntroduceHappening(Happening happening)
        {
            if (happening.Title == expectedHappening)
            {
                DoBegin1();
            }
        }

        private void DoBegin1()
        {
            happeningManager.OnLaunchHappening -= CheckCampIntroduceHappening;
            dialogModelDecorator.OnShowAnswers += DoBegin2;
        }


        private void DoBegin2()
        {
            dialogModelDecorator.OnShowAnswers -= DoBegin2;
            OnLaunchStep?.Invoke(this);
            popup = popupManager.ShowPopup(popupType) as TutorialPopup;
            popup.OnFinish += Finish;
        }

        public override void FinishGame()
        {
            happeningManager.OnLaunchHappening -= CheckCampIntroduceHappening;
            if (dialogModelDecorator.IsModel)
                dialogModelDecorator.OnShowAnswers -= DoBegin2;
        }
    }
}
