using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.UI
{
    abstract class TextUI : MonoBehaviour
    {
        public abstract string GetText();
        public abstract void SetText(string text);
        public abstract void SetTextColor(Color color);
    }
}
