using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Game.Configurations
{
    //Это класс посредник нужен для того, чтобы можно было менять некоторые параметры в игре, не меняя их в скриптблобжект настройках
    public class ConfigurationRuntime
    {
        public StartSceneConfig StartSceneConfig { get; } = new StartSceneConfig();
        public PlotConfig PlotConfig { get; }
        public TutorialConfig TutorialConfig { get; } 
        public SaveConfing SaveConfing { get; }

        public ConfigurationRuntime(StartSceneConfig startSceneConfig, PlotConfig plotConfig, TutorialConfig tutorialConfig, SaveConfing saveConfing)
        {
            StartSceneConfig.startScene = startSceneConfig.startScene;
            StartSceneConfig.fullStart = startSceneConfig.fullStart;
            PlotConfig = plotConfig.Clone();
            TutorialConfig = tutorialConfig.Clone();
            SaveConfing = saveConfing.Clone();  
        }
    }
}
