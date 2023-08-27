using Assets.Game.Core;
using Assets.Game.Plot.Core;
using System;

namespace Assets.Game.Plot.Steps
{
    //14
    class PStep14LeaveCastle : PlotStep
    {
        private readonly SetupCampManager setupCampManager;

        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;
        public PStep14LeaveCastle(SetupCampManager setupCampManager)
        {
            this.setupCampManager = setupCampManager;
            this.stepType = PlotStepType.LeaveCastle;
        }

        public override void Begin()
        {
            setupCampManager.LeaveCamp();
            OnLaunchStep?.Invoke(this);
            Finish();
        }

        public override void Finish()
        {
            OnFinishStep?.Invoke();
        }
    }

    
}
