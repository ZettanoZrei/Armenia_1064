using Assets.Game.Configurations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Plot.UI
{
    class PlotWordPresentor
    {
        private readonly PlotWordsPopup plot;
        private readonly int closeTime;
        private readonly string text;

        public event Action OnFinish;
        public PlotWordPresentor(PlotWordsPopup plot, int closeTime, string text)
        {
            this.plot = plot;
            this.closeTime = closeTime;
            this.text = text;
        }

        public async void Begin()
        {
            plot.OnTextFade += Finish;
            plot.ShowText(text);
            await Task.Delay(closeTime * 1000);
            plot.FadeText();
        }

        private void Finish()
        {
            plot.OnTextFade -= Finish;
            OnFinish?.Invoke();
        }       
    }
}
