using UnityEngine;

namespace Assets.Game.Plot.Scripts
{
    class PlotMapViewComponent : MonoBehaviour 
    {
        [SerializeField] private GameObject map;

        public GameObject Map => map; 
    }
}
