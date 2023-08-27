using Assets.GameModules.Narrative;
using UnityEngine;

namespace Assets.Game.Plot.Core
{
    class NarrativeObserverActions: MonoBehaviour
    {
        public void Activate(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }
        public void Deactivate(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
