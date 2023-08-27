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
        private Transform popupContainer;

        [Inject]
        private PopupCatalog happenViewsCatalog;

        public void InjectPopupContainer(Transform popupContainer)
        {
            this.popupContainer = popupContainer;
        }
        internal IPopup GetPopup(PopupType popupName)
        {            
            return MonoBehaviour.Instantiate(happenViewsCatalog.GetPopup(popupName), popupContainer);

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
