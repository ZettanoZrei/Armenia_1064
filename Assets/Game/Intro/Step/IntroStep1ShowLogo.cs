using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.Plot.Core;
using Assets.Game.Plot.UI;
using ExtraInjection;
using System;
using UniRx;
using UnityEngine.SceneManagement;

namespace Assets.Game.Intro.Step
{
    class IntroStep1ShowLogo : IntroStep, IExtraInject
    {
        private readonly IntroConfig config;
        [ExtraInject] private ShowUIElementsModel showUIElementsModel;

        public override event Action OnFinishStep;
        public override event Action<IStep<IntroStepType>> OnLaunchStep;

        public IntroStep1ShowLogo(IntroConfig config)
        {
            this.config = config;
            stepType = IntroStepType.Logo;

        }

        public override void Begin()
        {
            OnLaunchStep?.Invoke(this);
            showUIElementsModel.Init(PopupType.IntroLogo, 0.2f, config.logoAppearTime, config.logoStayTime, config.logoFadeTime);
            showUIElementsModel.Begin();
            showUIElementsModel.OnFinish += Finish;
        }


        public override void Finish()
        {           
            OnFinishStep?.Invoke();
        }

        public override void Break()
          {
            showUIElementsModel.Finsih();
        }
    }
}
