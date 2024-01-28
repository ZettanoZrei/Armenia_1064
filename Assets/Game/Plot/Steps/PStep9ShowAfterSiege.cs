using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.Plot.Core;
using Assets.Game.Plot.UI;
using Assets.GameEngine;
using Assets.Modules;
using GameSystems.Modules;
using System;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Game.Plot.Steps
{
    //9
    class PStep9ShowAfterSiege : PlotStep, IInitializable, IGameFinishElement
    {
        private readonly PopupManager popupManager;
        private readonly SignalBus signalBus;
        private readonly PlotConfig plotConfig;
        private PlotWordPresentor presentor;

        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;
        private readonly string text = "Спустя три недели...";

        public PStep9ShowAfterSiege(PopupManager popupManager, ConfigurationRuntime config, SignalBus signalBus)
        {
            this.stepType = PlotStepType.AfterSiege;
            this.popupManager = popupManager;
            this.signalBus = signalBus;
            this.plotConfig = config.PlotConfig;
        }
        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        void IGameFinishElement.FinishGame()
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
