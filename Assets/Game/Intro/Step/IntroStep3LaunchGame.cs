using Assets.Game.Core;
using System;

namespace Assets.Game.Intro.Step
{
    class IntroStep3LaunchGame : IntroStep
    {
        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<IntroStepType>> OnLaunchStep;
        public IntroStep3LaunchGame()
        {
            this.stepType = IntroStepType.Launch;
        }

        public override void Begin()
        {
            OnLaunchStep?.Invoke(this);
            MySceneManager.LoadSceneForBegin(Scene.MainMenuScene);
            Finish();
        }

        public override void Finish()
        {
            OnFinishStep?.Invoke();
        }
    }
}
