using UnityEngine;
using System;
using Interfeces;

namespace Player
{
    public class TriggerComponent : MonoBehaviour, ITriggerComponent
    {
        [SerializeField]
        private TriggerReceiver triggerReceiver;

        public event Action<Collider2D> OnTriggerStay
        {
            add { triggerReceiver.OnTriggerStayEvent += value; }
            remove { triggerReceiver.OnTriggerStayEvent -= value; }
        }

        public event Action<Collider2D> OnTriggerEnter
        {
            add { triggerReceiver.OnTriggerEnterEvent += value; }
            remove { triggerReceiver.OnTriggerEnterEvent -= value; }
        }

        public event Action<Collider2D> OnTriggerExit
        {
            add { triggerReceiver.OnTriggerExitEvent += value; }
            remove { triggerReceiver.OnTriggerExitEvent -= value; }
        }
    }
}
