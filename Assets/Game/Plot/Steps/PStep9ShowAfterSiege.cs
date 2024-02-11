using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.Plot.Core;
using Assets.Game.Plot.UI;
using Assets.GameEngine;
using Assets.Modules;
using ExtraInjection;
using GameSystems.Modules;
using System;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Game.Plot.Steps
{
    //9
    class PStep9ShowAfterSiege : PlotStep, IExtraInject, IGameLeave
    {
        [ExtraInject] private PopupManager popupManager;
        private readonly PlotConfig plotConfig;
        private PlotWordPresentor presentor;

        public override event Action OnFinishStep;
        public override event Action<IStep<PlotStepType>> OnLaunchStep;
        private readonly string text = "Спустя три недели...";

        public PStep9ShowAfterSiege(ConfigurationRuntime config)
        {
            this.stepType = PlotStepType.AfterSiege;
            this.plotConfig = config.PlotConfig;
        }

        void IGameLeave.LeaveGame()
        {
            if (presentor != null)
                presentor.OnFinish -= Finish;
        }
        public override void Begin()
        {
            var popup = popupManager.ShowPopup(PopupType.PlotDarkWords) as PlotWordsPopup;
            presentor = new PlotWordPresentor(popup, plotConfig.darkWordTime, text);
            presentor.OnFinish += Finish;
            presentor.Begin();
            OnLaunchStep?.Invoke(this);
        }

        public override void Finish()
        {
            presentor.OnFinish -= Finish;
            popupManager.ClosePopup(PopupType.PlotDarkWords);
            //sceneManager.LoadCamp();
            OnFinishStep?.Invoke();
        }
    }
}
