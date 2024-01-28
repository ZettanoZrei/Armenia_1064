using Assets.Game.Core;
using Assets.Game.Tutorial.Core;
using Assets.Game.Tutorial.UI;
using Assets.GameEngine;
using GameSystems.Modules;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Game.Tutorial.Steps
{
    class StepShowInterfaceInstructions : StepShowPopup
    {
        private readonly MySceneManager sceneManager;

        public override event Action<INarrativeStep<TutorialStepType>> OnLaunchStep;
        public StepShowInterfaceInstructions(PopupManager popupManager, MySceneManager sceneManager, PopupType popupType, SignalBus signalBus)
            : base(popupManager, popupType, signalBus)
        {
            this.stepType = TutorialStepType.InterfaceInstructions;
            this.sceneManager = sceneManager;
        }

        public override void Begin()
        {
            //sceneManager.OnChangeScene += CheckScene;
             DoBegin();
        }

        private async Task DoBegin()
        {
            //sceneManager.OnChangeScene -= CheckScene;
            await Task.Delay(1000);
            OnLaunchStep?.Invoke(this);
            popup = popupManager.ShowPopup(popupType) as TutorialPopup;
            popup.OnFinish += Finish;
        }

        public override void FinishGame()
        {
            if (popup != null)
                popup.OnFinish -= Finish;
        }
    }
}
