using Entities;
using System;
using UnityEngine;

namespace Assets.Game
{
    [CreateAssetMenu(
        fileName = "HappenViewCatalog",
        menuName = "View/HappenViewCatalog"
    )]
    public class PopupCatalog : ScriptableObject
    {
        [SerializeField]
        private Popup[] popups;

        public Popup GetPopup(PopupType popupType)
        {
            foreach (var popup in popups)
            {
                if (popup.PopupType == popupType)
                {
                    return popup;
                }
            }
            throw new Exception($"can't find popup {popupType}");
        }
    }
}
