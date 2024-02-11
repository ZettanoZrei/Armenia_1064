using Assets.Game.Configurations;
using Assets.Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Loader
{
    public class TaskLaunchGame : IStep<LoadStepType>
    {       
        private readonly StartSceneConfig startSceneConfig;
        private readonly IMenuCommand newGameCoomand;
        private readonly IntroConfig introConfig;

        public TaskLaunchGame(ConfigurationRuntime configurationRuntime, IntroConfig introConfig, IEnumerable<IMenuCommand> menuCommands)
        {
            this.startSceneConfig = configurationRuntime.StartSceneConfig;
            this.newGameCoomand = menuCommands.First(x => x is NewGameCommand);
            this.introConfig = introConfig;
        }

        public LoadStepType StepType => LoadStepType.LoadHappening;

        public event Action OnFinishStep;
        public event Action<IStep<LoadStepType>> OnLaunchStep;

        public void Begin()
        {
            if (introConfig.activate)
                MySceneManager.LoadSceneForBegin(Scene.PlotScene);
            else if (startSceneConfig.fullStart)
                MySceneManager.LoadSceneForBegin(Scene.MainMenuScene);
            else
                newGameCoomand.Execute();
        }

        public void Finish()
        {

        }
    }
}
