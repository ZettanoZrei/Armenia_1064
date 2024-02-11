using Assets.Game.Core;
using Assets.Game.HappeningSystem;
using Assets.Game.Tutorial.Core;
using Assets.Game.Tutorial.UI;
using Assets.GameEngine;
using Entities;
using Model.Entities.Happenings;
using System;
using Zenject;

namespace Assets.Game.Tutorial.Steps
{
    class StepShowRest : StepShowPopup
    {
        private readonly HappeningManager happeningManager;
        private string expectedHappening;
        private readonly MySceneManager sceneManager;

        public override event Action<IStep<TutorialStepType>> OnLaunchStep;

        public StepShowRest(PopupType popupType, HappeningManager happeningManager,
             MySceneManager sceneManager, SignalBus signalBus) : base(popupType, signalBus)
        {
            this.stepType = TutorialStepType.Rest;
            this.happeningManager = happeningManager;
            this.expectedHappening = "Эпизод_2.1";
            this.sceneManager = sceneManager;
        }


        public override void Begin()
        {
            happeningManager.OnFinishHappening += CheckCampIntroduceHappening;
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
            happeningManager.OnFinishHappening -= CheckCampIntroduceHappening;
            sceneManager.OnChangeScene_Post += CheckReturnToCamp;
        }
        private void CheckReturnToCamp(Scene scene)
        {
            if (scene == Scene.CampScene)
                DoBegin2();
        }

        private void DoBegin2()
        {
            sceneManager.OnChangeScene_Post -= CheckReturnToCamp;
            OnLaunchStep?.Invoke(this);
            popup = popupManager.ShowPopup(popupType) as TutorialPopup;
            popup.OnFinish += Finish;
        }

        public override void FinishScene()
        {
            happeningManager.OnFinishHappening -= CheckCampIntroduceHappening;
            sceneManager.OnChangeScene_Post -= CheckReturnToCamp;

            if (popup != null)
                popup.OnFinish -= Finish;
        }
    }
}
