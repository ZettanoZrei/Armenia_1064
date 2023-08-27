using Assets.Game.HappeningSystem;
using Model.Entities.Answers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Menu
{
    class MenuView : Popup
    {
        [SerializeField] private OptionComponent optionComponent;
        public event Action<int> OnDecitionMade
        {
            add { optionComponent.OnDecitionMade += value; }
            remove { optionComponent.OnDecitionMade -= value; }
        }

        public void SetOption(List<Answer> answers)
        {
            optionComponent.UpdateOptions(answers);
        }
    }
}
