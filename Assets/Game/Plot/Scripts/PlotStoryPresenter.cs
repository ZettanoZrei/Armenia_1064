using System;
using UnityEngine;

namespace Assets.Game.Plot.UI
{
    class PlotStoryPresenter
    {
        public event Action OnFinish;
        private readonly PlotStoryPopup view;
        private readonly PlotStoryModel model;

        public PlotStoryPresenter(PlotStoryPopup view, PlotStoryModel model)
        {
            this.view = view;
            this.model = model;
        }
        public void Begin()
        {
            NextText();

            view.OnCloseClick += Finish;
            view.OnNextText += NextText;
        }

        private void Finish()
        {
            view.OnCloseClick -= Finish;
            view.OnNextText -= NextText;
            OnFinish.Invoke();
        }

        private void NextText()
        {
            if (model.TryGetText(out string text))
            {
                view.ShowText(text);
            }
            else
            {
                view.ShowCloseButton();
            }
        }
    }
}
