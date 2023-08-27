using Assets.Game.Core;
using Assets.Game.Plot.Core;
using Assets.Game.Tutorial.Core;
using Model.Entities;
using System;

namespace Assets.Game.Configurations
{
    [Serializable]
    public class PlotConfig : INarrativeConfig, IClone<PlotConfig>
    {
        public bool activate;
        public PlotStepType startStep;
        public int fistWordsTime;
        public int extraDialogPhraseTime;
        public int darkWordTime;
        public bool Activate => activate;
        public int StartStep => (int)startStep;

        public MapPlot mapPlot;
        public GameTitleConfig gameTitlePlot;

        public PlotConfig Clone()
        {
            return new PlotConfig
            {
                activate = activate,
                startStep = startStep,
                fistWordsTime = fistWordsTime,
                extraDialogPhraseTime = extraDialogPhraseTime,
                darkWordTime = darkWordTime,
                mapPlot = new MapPlot
                {
                    aimScale = mapPlot.aimScale,
                    stepMax = mapPlot.stepMax,
                    stepMin = mapPlot.stepMin,
                    timeInterval = mapPlot.timeInterval,
                }
            };
        }
    }

    [Serializable]
    public class GameTitleConfig
    {
        public float startDelayTime;
        public float appearTime;
        public float stayTime;
        public float fadeTime;
        public float blackoutTime;
    }
}
