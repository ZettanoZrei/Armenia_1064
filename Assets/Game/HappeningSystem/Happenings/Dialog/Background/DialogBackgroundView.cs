using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.HappeningSystem
{
    class DialogBackgroundView : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        public DialogBackgroundViewType dialogBackgroundViewType;
        public void SetBackground(Sprite sprite)
        {
            image.sprite = sprite;
        }       

        public enum DialogBackgroundViewType
        {
            Front,
            Back
        }
    }
}
