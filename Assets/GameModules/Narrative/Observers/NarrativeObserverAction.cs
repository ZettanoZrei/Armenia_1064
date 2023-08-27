using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.GameModules.Narrative
{
    [Serializable]
    class NarrativeObserverAction 
    {
        public GameObject observed;
        public UnityEvent<GameObject> unityEvent;

        public void Execute()
        {
            unityEvent?.Invoke(observed);
        }
    }
}
