using GameSystems.Modules;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Modules
{
    public class SceneManager : MonoBehaviour
    {
        private SignalBus signalBus;
        public SceneState GameState { get; private set; }
        private readonly List<IGameElement> gameElements = new List<IGameElement>();

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        private void OnEnable()
        {
            signalBus.Subscribe<ConnectGameElementEvent>(AddElement);
        }

        private void OnDisable()
        {
            signalBus.Unsubscribe<ConnectGameElementEvent>(AddElement);
        }

        private void AddElement(ConnectGameElementEvent e)
        {
            if (!gameElements.Contains(e.GameElement))
            {
                gameElements.Add(e.GameElement);
            }
        }

        public void InitGame()
        {
            GameState = SceneState.Init;
            for (var i = 0; i < gameElements.Count; i++)
            {
                if (gameElements[i] is IGameInitElement gameElement)
                {
                    gameElement.InitGame();
                }
            }
        }
        public void ReadyGame()
        {
            GameState = SceneState.Ready;
            for (var i = 0; i < gameElements.Count; i++)
            {
                if (gameElements[i] is IGameReadyElement gameElement)
                {
                    gameElement.ReadyGame();
                }
            }
        }
        public void StartGame()
        {
            GameState = SceneState.Played;
            for (var i = 0; i < gameElements.Count; i++)
            {
                if (gameElements[i] is IGameStartElement gameElement)
                {
                    gameElement.StartGame();
                }
            }
        }
        public void EndGame()
        {
            GameState = SceneState.Finish;
            for (var i = 0; i < gameElements.Count; i++)
            {
                if (gameElements[i] is IGameFinishElement gameElement)
                {
                    gameElement.FinishGame();
                }
            }
        }
        public void PauseGame()
        {
            GameState = SceneState.Pause;
            for (var i = 0; i < gameElements.Count; i++)
            {
                if (gameElements[i] is IGamePauseElement gameElement)
                {
                    gameElement.PauseGame();
                }
            }
        }
        public void ContinueGame()
        {
            GameState = SceneState.Played;
            for (var i = 0; i < gameElements.Count; i++)
            {
                if (gameElements[i] is IGameResumeElement gameElement)
                {
                    gameElement.ResumeGame();
                }
            }
        }
    }
}
