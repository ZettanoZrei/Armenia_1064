using TMPro;
using UnityEngine;

namespace Assets.Game.UI
{
    class TextProUI : TextUI
    {
        [SerializeField] private TMP_Text uText;
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
