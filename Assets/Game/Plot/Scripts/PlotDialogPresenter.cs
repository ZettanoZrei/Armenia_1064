using Assets.Game.Configurations;
using Assets.Game.Plot.Scripts;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Plot.UI
{
    class PlotDialogPresenter
    {
        private readonly PlotDialogPopup popup;
        private readonly PlotDialogModel model;
        private readonly PlotConfig plotConfig;

        public event Action OnFinish;
        public PlotDialogPresenter(PlotDialogPopup popup, PlotDialogModel model, PlotConfig config)
        {
            this.popup = popup;
            this.model = model;
            this.plotConfig = config;
        }

        public void Begin()
        {
            model.ResetCount();
            Speak();
        }
        public void Speak()
        {
            if (model.TryGetPhrase(out var phrase))
            {
                popup.Show(phrase);
                popup.StartCoroutine(Timer(phrase.Time));
            }
            else
            {
                OnFinish?.Invoke();
            }
        }

        private IEnumerator Timer(int time)
        {
            yield return new WaitForSeconds(time + plotConfig.extraDialogPhraseTime);
            Speak();
        }
    }
}
