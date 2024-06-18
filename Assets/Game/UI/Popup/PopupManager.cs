using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

namespace Assets.Game
{
    public class PopupManager
    {
        private Dictionary<PopupType, PopupInfo> activePopup = new Dictionary<PopupType, PopupInfo>();
        public event EventHandler<PopupChangedEventArgs> OnPopupChanged;

        private BlockCurtain blockCurtain;
        private PopupFabrica popupFabrica;

        public PopupManager(PopupFabrica popupFabrica, BlockCurtain blockCurtain)
        {
            this.popupFabrica = popupFabrica;
            this.blockCurtain = blockCurtain;
        }

        internal IPopup ShowPopup(PopupType popupType, bool pause = true)
        {
            if (IsPopupActive(popupType))
                throw new Exception($"popup: {popupType} is used already");

            
            Debug.Log($"SHOW POPUP TYPE: {popupType}");
            var popup = popupFabrica.GetPopup(popupType);
            activePopup[popupType] = new PopupInfo { Popup = popup, Pause = pause };
            popup.Activate();

            blockCurtain.gameObject.SetActive(true);
            OnPopupChanged?.Invoke(this, GeneratePopupEvent(popupType, true));
            //OnAnyPopupShown?.Invoke();
            return popup;
        }


        internal void ClosePopup(PopupType popupType)
        {
            if (!IsPopupActive(popupType))
                return;

            var popup = activePopup[popupType].Popup;
            activePopup.Remove(popupType);

            popup.Hide();
            //MonoBehaviour.Destroy((popup as MonoBehaviour).gameObject);
            OnPopupChanged?.Invoke(this, GeneratePopupEvent(popupType, false));
            if (activePopup.Count == 0)
            {
                blockCurtain.gameObject.SetActive(false);
            }
        }

        private bool IsPopupActive(PopupType popupName)
        {
            return activePopup.ContainsKey(popupName);
        }

        public void ClearCache()
        {
            activePopup.Clear();
        }

        public bool CheckActivePausePopups()
        {
            return activePopup.Values.Any(x => x.Pause);
        }

        private PopupChangedEventArgs GeneratePopupEvent(PopupType popupType, bool isShown)
        {
            bool pause = activePopup.Any(x => x.Value.Pause);
            return new PopupChangedEventArgs
            {
                Popup = popupType,
                Pause = pause,
                IsShown = isShown
            };
        }

        struct PopupInfo
        {
            public IPopup Popup { get; set; }
            public bool Pause { get; set; }
        }

        public class PopupChangedEventArgs : EventArgs
        {
            public PopupType Popup { get; set; }
            public bool IsShown { get; set; }
            public bool Pause { get; set; }
        }
    }
}





