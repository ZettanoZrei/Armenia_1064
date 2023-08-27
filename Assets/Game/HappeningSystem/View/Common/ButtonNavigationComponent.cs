using Assets.Modules.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Game.HappeningSystem.View.Common
{
    internal class ButtonNavigationComponent : MonoBehaviour
    {
        [SerializeField] private SimpleButton closeButton;
        [SerializeField] private SimpleButton nextTextButton;
       
        public event Action OnClose
        {
            add { closeButton.OnClick += value; }
            remove { closeButton.OnClick -= value; }
        }

        public event Action OnNextPhrase
        {
            add { nextTextButton.OnClick += value; }
            remove { nextTextButton.OnClick -= value; }
        }

        public void SetActiveCloseButton(bool value)
        {
            closeButton.SetActiveObject(value);
        }

        public void SetActiveNextButton(bool value)
        {
            nextTextButton.SetActiveObject(value);
        }
    }
}
