using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.Plot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Game.Plot.Steps
{
    //0
    internal class PStep0ChangeStartScene : PlotStep
    {
        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;

        private readonly StartSceneConfig sceneConfig;
        private readonly MySceneManager sceneManager;

        public PStep0ChangeStartScene(ConfigurationRuntime configurationRuntime, MySceneManager sceneManager)
        {
            this.sceneConfig = configurationRuntime.StartSceneConfig;
            this.sceneManager = sceneManager;
            this.stepType = PlotStepType.ChangeStartScene;
        }

        public override void Begin()
        {
            sceneConfig.startScene = Scene.PlotScene;
            sceneManager.OnChangeScene_Post += CheckPlotScene;
            OnLaunchStep?.Invoke(this);
        }

        private void CheckPlotScene(Scene scene)
        {
            if (scene == Scene.PlotScene)
                Finish();
        }

        public override void Finish()
        {
            sceneManager.OnChangeScene_Post -= CheckPlotScene;
            OnFinishStep?.Invoke();
        }
    }
}
