using Assets.Game.Core;
using Assets.Game.Intro;
using System;
using Zenject;

namespace Loader
{
    class TaskStartIntro : IStep<LoadStepType>
    {
        private readonly IntroManager introManager;

        public TaskStartIntro(IntroManager introManager)
        {
            this.introManager = introManager;
        }

        public LoadStepType StepType => LoadStepType.StartIntro;

        public event Action OnFinishStep;
        public event Action<IStep<LoadStepType>> OnLaunchStep;

        public void Begin()
        {
            introManager.Begin();
        }

        public void Finish()
        {

        }
    }
}
