using Assets.Game.Plot.Scripts;
using Assets.Modules.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Plot.UI
{
    class PlotDialogPopup : Popup
    {
        [SerializeField] private GameObject barsegIcon;
        [SerializeField] private GameObject vasiliyIcon;
        [SerializeField] private Text uText;
        public void Show(PhrasesInfo phrase)
        {
            uText.text = phrase.Phrase;
            barsegIcon.SetActive(phrase.Person == "Барсег");
            vasiliyIcon.SetActive(!(phrase.Person == "Барсег"));
        }
    }
}
