using Assets.Game.Camp;
using Assets.Game.Core;
using Assets.Game.Plot.Core;
using Assets.GameEngine;
using System;

namespace Assets.Game.Plot.Steps
{
    //10
    class PStep10ChangeCampSprite : PlotStep, ILeaveGameComponentDI
    {
        private readonly SetupCampManager campManager;
        private readonly CampIncomingData incomingData;
        private readonly MySceneManager sceneManager;

        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;

        public PStep10ChangeCampSprite(SetupCampManager campManager, CampIncomingData incomingData, MySceneManager sceneManager, GameSystemDIController zenjectGameSystem)
        {
            this.stepType = PlotStepType.ChangeCampSprite;
            this.campManager = campManager;
            this.incomingData = incomingData;
            this.sceneManager = sceneManager;
            zenjectGameSystem.AddComponent(this);
        }

        public override void Begin()
        {
            incomingData.CampImagePrefab = "АревбердВОгне";
            campManager.SetupCamp();

            sceneManager.OnChangeScene_Post += DoFinish;
            OnLaunchStep?.Invoke(this);
        }

        private void DoFinish(Scene _)
        {
            Finish();
        }

        public override void Finish()
        {
            sceneManager.OnChangeScene_Post -= DoFinish;
            OnFinishStep?.Invoke();
        }

        void ILeaveGameComponentDI.LeaveGame()
        {
            sceneManager.OnChangeScene_Post -= DoFinish;
        }
    }
}
