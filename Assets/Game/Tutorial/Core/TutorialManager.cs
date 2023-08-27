using Assets.Game.Configurations;
using Assets.Game.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Tutorial.Core
{
    public class TutorialManager : NarrativeManager<TutorialStepType>, IInitializable, ILateDisposable
    {
        public TutorialManager(List<INarrativeStep<TutorialStepType>> tutorialSteps, ConfigurationRuntime config) : base(tutorialSteps)
        {
            this.config = config.TutorialConfig;
        }

        void IInitializable.Initialize()
        {
            Init();
        }
        void ILateDisposable.LateDispose()
        {
            Finish();
        }
    }
}
