using Assets.Game.HappeningSystem;
using Assets.Game.UI.EndPopupSystem;
using Assets.Modules.UI;
using Model.Entities.Happenings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.UI.FailGameSystem
{
    internal class GameOverPopup : Popup
    {
        [SerializeField] private SimpleButton toMenuButton;

        public event Action OnClick
        {
            add { toMenuButton.OnClick += value; }
            remove { toMenuButton.OnClick -= value; }
        }
    }
}
