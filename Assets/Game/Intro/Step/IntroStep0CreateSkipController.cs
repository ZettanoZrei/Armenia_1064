using Assets.Game.Core;
using System;
using UnityEngine;

namespace Assets.Game.Intro.Step
{
    class IntroStep0CreateSkipController : IntroStep
    {
        private readonly MySceneManager sceneManager;
        private readonly InputIntroController.Factory factory;

        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<IntroStepType>> OnLaunchStep;
        public IntroStep0CreateSkipController(MySceneManager sceneManager, InputIntroController.Factory factory)
        {
            this.stepType = IntroStepType.Skip;
            this.sceneManager = sceneManager;
            this.factory = factory;
        }

        
        public override void Begin()
        {
            if (sceneManager.CurrentScene == Scene.PlotScene)
                DoBegin(Scene.PlotScene);
            else
                sceneManager.OnChangeScene_Post += DoBegin;
        }

        private void DoBegin(Scene scene)
        {
            if (scene != Scene.PlotScene)
                return;
            factory.Create();
            sceneManager.OnChangeScene_Post -= DoBegin;
            OnLaunchStep?.Invoke(this);
            Finish();

        }

        public override void Finish()
        {
            OnFinishStep?.Invoke();
        }
    }
}
