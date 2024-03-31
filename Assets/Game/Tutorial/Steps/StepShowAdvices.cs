using Assets.Game.Core;
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
        public override event Action<IStep<TutorialStepType>> OnLaunchStep;
        protected string expectedHappening;


        public StepShowAdvices(PopupType popupType, SignalBus signalBus) : base(popupType, signalBus)
        {
            this.stepType = TutorialStepType.Advices;
            this.expectedHappening = "Эпизод_1.2";
        }

        public override void Begin()
        {
            //happeningManager.OnLaunchHappening += CheckCampIntroduceHappening;
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
            //happeningManager.OnLaunchHappening -= CheckCampIntroduceHappening;
            //dialogModelDecorator.OnShowAnswers += DoBegin2;
        }


        private void DoBegin2()
        {
            //dialogModelDecorator.OnShowAnswers -= DoBegin2;
            OnLaunchStep?.Invoke(this);
            popup = popupManager.ShowPopup(popupType) as TutorialPopup;
            popup.OnFinish += Finish;
        }

        public override void FinishScene()
        {
            //happeningManager.OnLaunchHappening -= CheckCampIntroduceHappening;
            //if (dialogModelDecorator.IsModel)
            //    dialogModelDecorator.OnShowAnswers -= DoBegin2;
        }
    }
}
