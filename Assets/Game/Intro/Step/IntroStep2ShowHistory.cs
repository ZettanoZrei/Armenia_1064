using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.Plot.UI;
using ExtraInjection;
using System;

namespace Assets.Game.Intro.Step
{
    class IntroStep2ShowHistory : IntroStep, IExtraInject
    {
        private readonly IntroConfig config;
        [ExtraInject] private ShowUIElementsModel showUIElementsModel;

        public override event Action OnFinishStep;
        public override event Action<IStep<IntroStepType>> OnLaunchStep;
        public IntroStep2ShowHistory(IntroConfig config)
        {
            this.stepType = IntroStepType.History;
            this.config = config;
        }

        public override void Begin()
        {
            OnLaunchStep?.Invoke(this);
            showUIElementsModel.Init(PopupType.IntroHistory, config.historyDelayTime, config.historyAppearTime, config.historyStayTime, config.historyFadeime);
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
