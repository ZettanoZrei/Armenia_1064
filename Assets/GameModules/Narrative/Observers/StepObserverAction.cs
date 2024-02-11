using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.GameModules.Narrative
{
    [Serializable]
    class StepObserverAction 
    {
        public GameObject observed;
        public UnityEvent<GameObject> unityEvent;

        public void Execute()
        {
            unityEvent?.Invoke(observed);
        }
    }
}
