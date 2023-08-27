using Assets.Game.Camp;
using Assets.Game.Core;
using Assets.Game.HappeningSystem;
using Assets.Game.Plot.Core;
using Assets.GameEngine;
using System;
using System.Threading.Tasks;

namespace Assets.Game.Plot.Steps
{
    //5.1
    class PStep5BeginAttack_1 : PlotStep, ILeaveGameComponentDI
    {
        private readonly CampIncomingData incomingData;
        private readonly HappeningManager happeningManager;

        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;
        public PStep5BeginAttack_1(CampIncomingData incomingData, HappeningManager happeningManager, GameSystemDIController zenjectGameSystem)
        {
            this.incomingData = incomingData;
            this.happeningManager = happeningManager;
            this.stepType = PlotStepType.BeginAttack_1;
            zenjectGameSystem.AddComponent(this);
        }
        
        public override void Begin()
        {
            incomingData.OnDialogAvailableChange += CheckIfAllDialogFinished;
        }
        private void CheckIfAllDialogFinished(int value)
        {
            if (value <= 0)
            {
                incomingData.OnDialogAvailableChange -= CheckIfAllDialogFinished;
                happeningManager.OnFinishHappeningAsync += DoFinish;
            }
        }

        private Task DoFinish()
        {
            happeningManager.OnFinishHappeningAsync -= DoFinish;
            Finish();
            return Task.CompletedTask;
        }

        public override void Finish()
        {
            OnFinishStep?.Invoke();
        }

        void ILeaveGameComponentDI.LeaveGame()
        {
            incomingData.OnDialogAvailableChange -= CheckIfAllDialogFinished;
            happeningManager.OnFinishHappeningAsync -= DoFinish;
        }
    }
}
