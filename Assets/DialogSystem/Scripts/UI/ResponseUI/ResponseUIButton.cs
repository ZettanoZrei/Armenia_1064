using PixelCrushers.DialogueSystem.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.DialogSystem.Scripts.UI.ResponseUI
{
    public class ResponseUIButton : StandardUIResponseButton, IMyResponseUI
    {
        void IMyResponseUI.SetState(bool enabled)
        {
            button.enabled = enabled;
            //todo implement view
        }
    }
}
