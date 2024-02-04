using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.Plot.Core;
using Assets.Game.Plot.UI;
using ExtraInjection;
using System;
using UnityEngine;

namespace Assets.Game.Plot.Steps
{
    //3
    class PStep3ShowFirstWords : PlotStep, IExtraInject
    {
        [ExtraInject] private PopupManager popupManager;
        private readonly PlotConfig plotConfig;
        private PlotWordsPopup popup;
        private PlotWordPresentor presentor;
        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;

        private readonly string text = "1064 год от рождества Христова.\r\nЗемли бывшего Васпураканского царства. Княжество Богуник.\r\n\r\nВы возвращаетесь на родину предков, впервые с тех пор,\r\nкак вас, еще ребенком, родители вывезли в Византию.";
        public PStep3ShowFirstWords(ConfigurationRuntime config)
        {
            this.stepType = PlotStepType.FirstWords;
            this.plotConfig = config.PlotConfig;
        }
        public override void Begin()
        {
            popup = popupManager.ShowPopup(PopupType.PlotWordsPopup, false) as PlotWordsPopup;
            presentor = new PlotWordPresentor(popup, plotConfig.fistWordsTime, text);
            presentor.OnFinish += Finish;
            OnLaunchStep?.Invoke(this);
            presentor.Begin();
        }


        public override void Finish()
        {
            presentor.OnFinish -= Finish;
            popupManager.ClosePopup(PopupType.PlotWordsPopup);
            OnFinishStep?.Invoke();
        }
    }
}
