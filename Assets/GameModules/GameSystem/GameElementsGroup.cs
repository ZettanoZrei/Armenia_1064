using System.Collections.Generic;
using UnityEngine;

namespace GameSystems
{
    class GameElementsGroup : MonoBehaviour, IGameElementsGroup
    {
        private List<MonoBehaviour> gameElements;

        //нельзя ставить галочку если это подгруппа другой группы, иначе она может дублироваться 
        [SerializeField]
        private bool autoLoading;

        public bool IsAutoLoading => autoLoading; 

        private void Awake()
        {
            gameElements = new List<MonoBehaviour>();
            if (transform.childCount == 0)
                return;
            foreach (Transform child in transform)
            {
                AddPrimitiveRecursively(child.gameObject);
            }            
        }

        
        public IEnumerable<T> GetElements<T>()
        {
            foreach (var element in gameElements)
            {
                if (element is T el)
                    yield return el;
            }
        }

        private void AddPrimitiveRecursively(GameObject gameObject)
        {
            if (!gameObject.activeSelf)
                return;

            var mono = gameObject.GetComponents<MonoBehaviour>();
            foreach (var component in mono)
            {
                if (component is IGameElement)
                    gameElements.Add(component);

                if (component is IGameElementsGroup)
                    return;
            }

            if (gameObject.transform.childCount == 0)
                return;

            foreach (Transform child in gameObject.transform)
            {
                AddPrimitiveRecursively(child.gameObject);
            }

        }
    }
}


