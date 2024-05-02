using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.DialogSystem.Scripts
{
    public class ActorEventObserver : MonoBehaviour
    {
        public event Action DialogStartEvent;
        public event Action DialogEndEvent;
        public event Action<Response[]> ShowResponseMenuEvent;
        public event Action<Subtitle> ShowSubtitleEvent;

        public void TriggerDialogStartEvent(Transform _)
        {
            DialogStartEvent?.Invoke();
        }

        public void TriggerDialogEndEvent() 
        {
            DialogEndEvent?.Invoke();
        }

        public void TriggerShowResponseMenuEvent(Response[] responses)
        {
            ShowResponseMenuEvent?.Invoke(responses);
        }

        public void TriggerShowSubtitleEvent(Subtitle subtitle)
        {
            ShowSubtitleEvent?.Invoke(subtitle);
        }
    }
}
