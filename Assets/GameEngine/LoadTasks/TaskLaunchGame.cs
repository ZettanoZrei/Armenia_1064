using Assets.Game.Configurations;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Loader
{
    public class TaskLaunchGame : IInitializable
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

        void IInitializable.Initialize()
        {
            if(introConfig.activate)            
                MySceneManager.LoadSceneForBegin(Scene.PlotScene);          
            else if (startSceneConfig.fullStart)
                MySceneManager.LoadSceneForBegin(Scene.MainMenuScene);
            else
                newGameCoomand.Execute();
        }
    }
}
