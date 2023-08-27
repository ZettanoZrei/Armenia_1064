using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Game.HappeningSystem
{
    class ButtonScrollView : MonoBehaviour
    {
        [SerializeField]
        private GameObject container;

        [SerializeField]
        private Button button;

        public bool IsActive => container.activeSelf;

        public void AddListener(UnityAction action)
        {
            button.onClick.AddListener(action);
        }

        public void CleanListeners()
        {
            button.onClick.RemoveAllListeners();
        }

        public void SetActive(bool value)
        {
            container.SetActive(value);
        }
    }
}
