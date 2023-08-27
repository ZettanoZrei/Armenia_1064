using Assets.Game.Core;
using Assets.Game.Tutorial.Core;
using Model.Entities;
using System;

namespace Assets.Game.Configurations
{
    [Serializable]
    public class TutorialConfig : INarrativeConfig, IClone<TutorialConfig>
    {
        public bool activate;
        public TutorialStepType startStep;

        public bool Activate => activate;
        public int StartStep => (int)startStep;

        public TutorialConfig Clone()
        {
            return new TutorialConfig
            {
                activate = activate,
                startStep = startStep
            };
        }
    }
}
