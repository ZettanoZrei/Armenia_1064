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

        public void TriggerDialogStartEvent()
        {
            DialogStartEvent?.Invoke();
        }

        public void TriggerDialogEndEvent() 
        {
            DialogEndEvent?.Invoke();
        }
    }
}
