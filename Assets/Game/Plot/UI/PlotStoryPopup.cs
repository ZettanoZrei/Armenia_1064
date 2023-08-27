using Assets.Game.HappeningSystem;
using Assets.Modules.UI;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Game.Plot.UI
{
    class PlotStoryPopup : Popup
    {
        [SerializeField] private SimpleButton buttonScrollView;
        [SerializeField] private SimpleButton closeButton;
        [SerializeField] private Text uText;

        public event Action OnCloseClick
        {
            add { closeButton.OnClick += value; }
            remove { closeButton.OnClick -= value; }
        }
        public event Action OnNextText
        {
            add { buttonScrollView.OnClick += value; }
            remove { buttonScrollView.OnClick -= value; }
        }

        public void ShowText(string text)
        {
            this.uText.text = text;
        }

        public void ShowCloseButton()
        {
            buttonScrollView.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
        }
    }
}
