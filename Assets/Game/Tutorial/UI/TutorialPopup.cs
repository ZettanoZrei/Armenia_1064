using Assets.Modules.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Tutorial.UI
{
    internal class TutorialPopup : Popup
    {
        [SerializeField] private SimpleButton closeButton;
        public event Action OnFinish;
        private void OnEnable()
        {
            closeButton.OnClick += InvokeOnFinish;
        }

        private void OnDisable()
        {
            closeButton.OnClick -= InvokeOnFinish;
        }

        private void InvokeOnFinish()
        {
            OnFinish?.Invoke();
        }
    }
}
