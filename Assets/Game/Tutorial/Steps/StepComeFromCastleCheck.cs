using Assets.Game.Core;
using Assets.Game.Tutorial.Core;
using Assets.GameEngine;
using System;

namespace Assets.Game.Tutorial.Steps
{
    class StepComeFromCastleCheck : TutorialStep, ILeaveGameComponentDI
    {
        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<TutorialStepType>> OnLaunchStep;

        private MySceneManager sceneManager;


        public StepComeFromCastleCheck(MySceneManager sceneManager, GameSystemDIController zenjectGameSystem)
        {
            this.sceneManager = sceneManager;
            stepType = TutorialStepType.CheckCastleLeave;
            zenjectGameSystem.AddComponent(this);
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

        void ILeaveGameComponentDI.LeaveGame()
        {
            sceneManager.OnChangeScene_Post -= CheckTravel_1Scene;
        }
    }
}
