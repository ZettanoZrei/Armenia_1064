using Assets.Game.Core;
using Assets.Game.Intro;
using Model.Entities;
using System;

namespace Assets.Game.Configurations
{
    [Serializable]
    public class IntroConfig : INarrativeConfig
    {
        public bool activate;
        public IntroStepType startStep;
        public bool Activate => activate;
        public int StartStep => (int)startStep;

        public float logoAppearTime;
        public float logoStayTime;
        public float logoFadeTime;

        public float historyDelayTime;
        public float historyAppearTime;
        public float historyStayTime;
        public float historyFadeime;
    }
}
