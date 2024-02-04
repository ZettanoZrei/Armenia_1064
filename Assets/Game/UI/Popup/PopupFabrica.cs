using Entities;
using GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Game
{
    public class PopupFabrica
    {
        private PopupContainer popupContainer;
        private PopupCatalog happenViewsCatalog;

        public PopupFabrica(PopupCatalog happenViewsCatalog, PopupContainer popupContainer)
        {
            this.happenViewsCatalog = happenViewsCatalog;
            this.popupContainer = popupContainer;
        }

        internal IPopup GetPopup(PopupType popupName)
        {            
            return MonoBehaviour.Instantiate(happenViewsCatalog.GetPopup(popupName), popupContainer.transform);

            //if (cache.TryGetValue(popupName, out IEntity popup))
            //{
            //    return popup;
            //}
            //else
            //{
            //    popup = Instantiate(happenViewsCatalog.GetPopup(popupName), popupContainer);
            //    cache[popupName] = popup;
            //    return popup;
            //}
        }

    }
}
