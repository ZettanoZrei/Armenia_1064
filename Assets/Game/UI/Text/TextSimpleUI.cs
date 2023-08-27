using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.UI
{
    class TextSimpleUI : TextUI
    {
        [SerializeField] protected Text uText;
        public override string GetText()
        {
            return uText.text;
        }

        public override void SetText(string text)
        {
            uText.text = text;
        }

        public override void SetTextColor(Color color)
        {
            uText.color = color;
        }
    }
}
