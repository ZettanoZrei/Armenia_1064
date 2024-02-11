using Assets.Game.Core;
using Assets.Game.Tutorial.Core;
using Assets.GameEngine;
using Assets.Modules;
using GameSystems.Modules;
using System;
using Zenject;

namespace Assets.Game.Tutorial.Steps
{
    class StepComeFromCastleCheck : TutorialStep, IGameLeave
    {
        public override event Action OnFinishStep;
        public override event Action<IStep<TutorialStepType>> OnLaunchStep;
        private MySceneManager sceneManager;

        public StepComeFromCastleCheck(MySceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
            stepType = TutorialStepType.CheckCastleLeave;
        }
        void IGameLeave.LeaveGame()
        {
            sceneManager.OnChangeScene_Post -= CheckTravel_1Scene;
        }
        public override void Begin()
        {
            sceneManager.OnChangeScene_Post += CheckTravel_1Scene;
            CheckTravel_1Scene(sceneManager.CurrentScene); //костыль
        }
        public override void Finish()
        {
            sceneManager.OnChangeScene_Post -= CheckTravel_1Scene;
            OnLaunchStep?.Invoke(this);
            OnFinishStep?.Invoke();
        }
        private void CheckTravel_1Scene(Scene scene)
        {
            if (scene == Scene.Travel_1)
            {
                Finish();
            }
        }
    }
}
