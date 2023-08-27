using Assets.Game.Core;
using Assets.Game.Plot.Core;
using System;
using UnityEngine;

namespace Assets.Game.Tutorial.Core
{
    abstract class TutorialStep : INarrativeStep<TutorialStepType>
    {
        public abstract event Action OnFinishStep;
        public abstract event Action<INarrativeStep<TutorialStepType>> OnLaunchStep;
        public TutorialStepType StepType => stepType;

        protected TutorialStepType stepType;

        public abstract void Begin();
        public abstract void Finish();
    }
}
