using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace GameSystems
{
    public class GameSystemContext
    {
        private readonly List<object> elementsCache = new List<object>();
        private readonly List<object> elements = new List<object>();

        public void LoadElements(IEnumerable<object> elemGroups)
        {
            foreach (var elem in elemGroups)
            {
                if (elem is IGameElementsGroup group)
                    AddElementRecurceively(group);
                else if (elem is IGameElement element)
                    AddElement(element);
            }
        }

        public void AddElement(object gameElement)
        {
            if (gameElement is IGameElement)
                elements.Add((IGameElement)gameElement);
        }

        private void AddElementRecurceively(IGameElementsGroup groups)
        {
            var elements = groups.GetElements<IGameElement>();
            foreach (var elem in elements)
            {
                if (elem is IGameElementsGroup group)
                    AddElementRecurceively(group);
                else if (elem is IGameElement element)
                    AddElement(element);
            }
        }

        public void RemoveElement(IGameElement element)
        {
            if (elements.Contains(element))
                elements.Remove(element);
        }

        public void InitGame(GameSystem gameSystem)
        {
            UpdateCache();
            foreach (var service in elementsCache)
            {
                if (service is IGameInitElement component)
                    component.InitGame(gameSystem);
            }
        }


        public void FinishGame()
        {
            UpdateCache();
            foreach (var service in elementsCache)
            {
                if (service is IGameFinishElement component)
                    component.FinishGame();
            }
        }


        public void PauseGame()
        {
            UpdateCache();
            foreach (var service in elementsCache)
            {
                if (service is IGamePauseElement component)
                    component.PauseGame();
            }
        }

        public void ResumeGame()
        {
            UpdateCache();
            foreach (var service in elementsCache)
            {
                if (service is IGameResumeElement component)
                    component.ResumeGame();
            }
        }
        public void ReadyGame()
        {
            UpdateCache();
            foreach (var service in elementsCache)
            {
                if (service is IGameReadyElement component)
                    component.ReadyGame();
            }
        }
        public void StartGame()
        {
            UpdateCache();
            foreach (var service in elementsCache)
            {
                if (service is IGameStartElement component)
                {
                    try
                    {
                        component.StartGame();
                    }
                    catch { }
                }
            }
        }
        public void ChangeScene()
        {
            UpdateCache();
            foreach (var service in elementsCache)
            {
                if (service is IGameChangeSceneElement component)
                    component.ChangeScene();
            }
        }
        private void UpdateCache()
        {
            elementsCache.Clear();
            elementsCache.AddRange(elements);
        }
    }
}
