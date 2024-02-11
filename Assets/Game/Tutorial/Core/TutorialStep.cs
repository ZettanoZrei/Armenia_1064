using Assets.Game.Core;
using Assets.Game.Plot.Core;
using System;
using UnityEngine;

namespace Assets.Game.Tutorial.Core
{
    abstract class TutorialStep : IStep<TutorialStepType>
    {
        public abstract event Action OnFinishStep;
        public abstract event Action<IStep<TutorialStepType>> OnLaunchStep;
        public TutorialStepType StepType => stepType;

        protected TutorialStepType stepType;

        public abstract void Begin();
        public abstract void Finish();
    }
}
