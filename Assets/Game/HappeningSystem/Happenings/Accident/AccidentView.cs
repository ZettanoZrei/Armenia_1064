using Assets.Game.HappeningSystem.View.Advice;
using Assets.Game.HappeningSystem.View.Common;
using Model.Entities.Answers;
using Model.Entities.Nodes;
using Model.Entities.Persons;
using Model.Entities.Phrases;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Assets.Game.HappeningSystem
{
    class AccidentView : Popup
    {
        [SerializeField] private OptionComponent optionView;
        [SerializeField] private TextScrollView textScrollView;
        [SerializeField] private AdvicePopupView advicePopupView;
        [SerializeField] private ButtonNavigationComponent buttonNavigation;

        public event Action<int> OnDecitionMade
        {
            add { optionView.OnDecitionMade += value; }
            remove { optionView.OnDecitionMade -= value; }
        }

        public event Action<PersonName> OnClickPortrait
        {
            add { advicePopupView.OnClickPortrait += value; }
            remove { advicePopupView.OnClickPortrait -= value; }
        }
        public event Action OnClose
        {
            add { buttonNavigation.OnClose += value; }
            remove { buttonNavigation.OnClose -= value; }
        }
        public event Action OnNextPhrase
        {
            add { buttonNavigation.OnNextPhrase += value; }
            remove { buttonNavigation.OnNextPhrase -= value; }
        }

        public void ShowAdvicePopup(List<PortraitButton> portraits)
        {
            advicePopupView.ShowAdvicePanel(portraits);
        }

        public void HideAdvicePopup()
        {
            advicePopupView.HideAdvicePanel();
        }

        public void UpdateText(Phrase phrase)
        {
            textScrollView.UpdateView(phrase);
        }
        public void UpdateOptions(List<Answer> answers)
        {
            optionView.UpdateOptions(answers);
        }

        public void ShowCloseButton(bool value)
        {
            buttonNavigation.SetActiveCloseButton(value);
        }

        public void ShowNextTextButton(bool value)
        {
            buttonNavigation.SetActiveNextButton(value);
        }
        public void Finish()
        {
            textScrollView.Finish();
        }

        public void InitPortraitHeap(PortaitHeap portraitHeap)
        {
            advicePopupView.InitPortraitHeap(portraitHeap);
        }
    }
}
