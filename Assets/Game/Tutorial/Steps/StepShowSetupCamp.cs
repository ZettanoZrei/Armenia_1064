using Assets.Game.Core;
using Assets.Game.HappeningSystem;
using Assets.Game.Tutorial.Core;
using Assets.Game.Tutorial.UI;
using Assets.GameEngine;
using Entities;
using Model.Entities.Happenings;
using System;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Game.Tutorial.Steps
{
    class StepShowSetupCamp : StepShowPopup
    {
        public override event Action<INarrativeStep<TutorialStepType>> OnLaunchStep;
        private readonly MySceneManager sceneManager;

        public StepShowSetupCamp(PopupManager popupManager, PopupType popupType, MySceneManager sceneManager, GameSystemDIController zenjectGameSystem)
            : base(popupManager, popupType, zenjectGameSystem)
        {
            this.stepType = TutorialStepType.SetupCamp;
            this.sceneManager = sceneManager;
        }

        public override void Begin()
        {
            sceneManager.OnChangeScene_Post += CheckReturnToTravelScene;
        }

        private void CheckReturnToTravelScene(Scene scene)
        {
            if (scene == Scene.Travel_1)
            {
                DoBegin();
            }
        }


        private async void DoBegin()
        {
            sceneManager.OnChangeScene_Post -= CheckReturnToTravelScene;

            await Delay();
            OnLaunchStep?.Invoke(this);
            popup = popupManager.ShowPopup(popupType) as TutorialPopup;
            popup.OnFinish += Finish;
        }

        private async Task Delay()
        {
            await Task.Delay(100);
        }

        public override void LeaveGame()
        {
            sceneManager.OnChangeScene_Post -= CheckReturnToTravelScene;
            if(popup!= null)
                popup.OnFinish -= Finish;
        }
    }
}
