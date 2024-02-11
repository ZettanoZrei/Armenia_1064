using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.Plot.Core;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Intro
{
    internal class IntroManager : StepManager<IntroStepType>, IInitializable, ILateDisposable
    {
        public IntroManager(List<IStep<IntroStepType>> steps, IntroConfig introConfig) : base(steps)
        {
            this.config = introConfig;
        }

        void IInitializable.Initialize()
        {
            Init();
        }

        void ILateDisposable.LateDispose()
        {
            Finish();
        }

        public void SkipIntroStep()
        {
            if (CurrentStepIndex > 0 && CurrentStepIndex < steps.Count())
                (steps[CurrentStepIndex] as IntroStep).Break();
        }
    }
}
