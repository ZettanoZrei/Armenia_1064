using Assets.Modules.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Game.UI.EndPopupSystem
{
    internal class EndGamePopup : Popup
    {
        [SerializeField] private SimpleButton toMainMenuButton;

        public event Action OnClick
        {
            add { toMainMenuButton.OnClick += value; }
            remove { toMainMenuButton.OnClick -= value; }
        }

    }
}
