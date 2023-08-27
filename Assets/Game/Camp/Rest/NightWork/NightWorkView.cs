using Assets.Game.HappeningSystem;
using Model.Entities.Answers;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Game.Camp
{
    class NightWorkView : Popup
    {
        [SerializeField] private OptionComponentsNightWork optionComponent;

        public event Action<int?> OnMadeDicition;

        private void OnEnable()
        {
            optionComponent.OnDecitionMade += DecitionMadeHandler;
        }

        private void OnDisable()
        {
            optionComponent.OnDecitionMade -= DecitionMadeHandler;
        }

        private void DecitionMadeHandler(int? answerNumber)
        {
            OnMadeDicition?.Invoke(answerNumber);
        }

        public void SetOption(List<NightWorkOptionInfo> options)
        {
            optionComponent.UpdateOptions(options);
        }
    }
}
