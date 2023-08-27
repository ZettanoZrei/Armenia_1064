using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Assets.GameEngine.Menu.Settings
{
    class SoundSettingView : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void SetSound(float value)
        {
            slider.value = value;
        }
        public void AddListener(UnityAction<float> listener)
        {
            slider.onValueChanged.AddListener(listener);
        }

        public void RemoveListener(UnityAction<float> listener)
        {
            slider.onValueChanged.RemoveListener(listener);
        }

    }
}
