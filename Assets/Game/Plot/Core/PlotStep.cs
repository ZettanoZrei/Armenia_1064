using Assets.Game.Core;
using Assets.Game.Tutorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Game.Plot.Core
{
    abstract class PlotStep : INarrativeStep<PlotStepType>
    {
        public abstract event Action OnFinishStep;
        public abstract event Action<INarrativeStep<PlotStepType>> OnLaunchStep;
        public PlotStepType StepType => stepType;

        protected PlotStepType stepType;

        public abstract void Begin();
        public abstract void Finish();
    }
}
