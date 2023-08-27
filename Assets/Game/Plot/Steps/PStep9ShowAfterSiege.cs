using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.Plot.Core;
using Assets.Game.Plot.UI;
using Assets.GameEngine;
using System;
using UnityEngine.SceneManagement;

namespace Assets.Game.Plot.Steps
{
    //9
    class PStep9ShowAfterSiege : PlotStep, ILeaveGameComponentDI
    {
        private readonly PopupManager popupManager;
        private readonly PlotConfig plotConfig;
        private PlotWordPresentor presentor;

        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;
        private readonly string text = "Спустя три недели...";

        public PStep9ShowAfterSiege(PopupManager popupManager, ConfigurationRuntime config, GameSystemDIController zenjectGameSystem)
        {
            this.stepType = PlotStepType.AfterSiege;
            this.popupManager = popupManager;
            this.plotConfig = config.PlotConfig;
            zenjectGameSystem.AddComponent(this);
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

        void ILeaveGameComponentDI.LeaveGame()
        {
            if(presentor!=null)
                presentor.OnFinish -= Finish;
        }
    }
}
