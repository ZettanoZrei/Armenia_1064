using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.Plot.Core;
using Loader;
using System;

namespace Assets.Game.Plot.Steps
{
    //2
    class PStep2BeginGame : PlotStep
    {
        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;

        private readonly MySceneManager sceneManager;
        private readonly StartSceneConfig startSceneConfig;
        private readonly TravelSceneNavigator travelSceneNavigator;

        public PStep2BeginGame(MySceneManager sceneManager, StartSceneConfig startSceneConfig, TravelSceneNavigator travelSceneNavigator)
        {
            this.sceneManager = sceneManager;
            this.startSceneConfig = startSceneConfig;
            this.travelSceneNavigator = travelSceneNavigator;
            this.stepType = PlotStepType.BeginGame;
        }

        public override void Begin()
        {
            sceneManager.OnChangeScene_Post += CheckFirstScene;
            travelSceneNavigator.LoadTravelScene();
            OnLaunchStep?.Invoke(this);
        }

        private void CheckFirstScene(Scene scene)
        {
            if (scene == startSceneConfig.startScene)
            {
                sceneManager.OnChangeScene_Post -= CheckFirstScene;
                Finish();
            }
        }

        public override void Finish()
        {
            OnFinishStep?.Invoke();
        }
    }
}
