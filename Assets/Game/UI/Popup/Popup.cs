using UnityEngine;

namespace Assets.Game
{
    public class Popup : MonoBehaviour, IPopup
    {
        [SerializeField] private GameObject popup;
        [SerializeField] private PopupType happeningType;
        public PopupType PopupType => happeningType;

        void IPopup.Activate()
        {
            popup.SetActive(true);
        }
        void IPopup.Hide()
        {
            popup.SetActive(false);
        }
    }
}
