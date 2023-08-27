using Assets.GameEngine.Other;
using Assets.Modules.UI;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Plot.UI
{
    class PlotWordsPopup : Popup
    {
        [SerializeField] private Text uText;
        [SerializeField] private FadeMonoScript fadeScript;

        public event Action OnTextFade
        {
            add { fadeScript.OnFinishFade += value; }
            remove { fadeScript.OnFinishFade -= value; }
        }

        public void ShowText(string text)
        {
            uText.text = text;
        }

        public void FadeText()
        {
            fadeScript.BeginFade(0.1f, 0.05f, uText);
        }
    }
}
