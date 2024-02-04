using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.HappeningSystem;
using Assets.Game.Plot.Core;
using Assets.Game.Plot.Scripts;
using Assets.Game.Plot.UI;
using ExtraInjection;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Plot.Steps
{
    //4
    class PStep4ShowDialog : PlotStep, IExtraInject
    {
        [ExtraInject] private PopupManager popupManager;
        private readonly PlotConfig config;
        private readonly PlotDialogModel model;
        private PlotDialogPopup popup;

        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;
        private PlotDialogPresenter dialogPresenter;
        public PStep4ShowDialog(ConfigurationRuntime config, PlotDialogModel model)
        {
            this.config = config.PlotConfig;
            this.model = model;
            this.stepType = PlotStepType.Dialog;
        }

        public override void Begin()
        {
            WorldState.Instance.StartCoroutine(BeginWithDelay());
        }

        private IEnumerator BeginWithDelay()
        {
            yield return new WaitForSeconds(1);
            popup = popupManager.ShowPopup(PopupType.PlotDialogPopup, false) as PlotDialogPopup;
            dialogPresenter = new PlotDialogPresenter(popup, model, config);
            dialogPresenter.OnFinish += Finish;
            OnLaunchStep?.Invoke(this);
            dialogPresenter.Begin();
        }

        public override void Finish()
        {
            popupManager.ClosePopup(PopupType.PlotDialogPopup);
            dialogPresenter.OnFinish -= Finish;
            OnFinishStep?.Invoke();
        }
    }
}
