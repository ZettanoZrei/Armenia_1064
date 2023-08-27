using Assets.GameModules.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Plot.UI
{
    class ShowUiElements : Popup
    {
        [SerializeField] private FadeOutUIComponent fadeOutUIComponent;  
        [SerializeField] private MaskableGraphic[] uiElements;

        public event Action OnFaded
        {
            add { fadeOutUIComponent.OnFaded += value; }
            remove { fadeOutUIComponent.OnFaded -= value; }
        }
        public event Action OnAppeared
        {
            add { fadeOutUIComponent.OnAppeared += value; }
            remove { fadeOutUIComponent.OnAppeared -= value; }
        }

        public void MakeTransparent()
        {
            foreach(var uiElement in uiElements)
            {
                uiElement.color = new Color(uiElement.color.r, uiElement.color.g, uiElement.color.b, 0);
            }
        }
        public void FadeOut(float duration)
        {
            fadeOutUIComponent.FadeOut(duration, uiElements);
        }

        public void Appear(float duration)
        {
            fadeOutUIComponent.Appear(duration, uiElements);
        }
    }
}
