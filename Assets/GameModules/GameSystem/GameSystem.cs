using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace GameSystems
{
    public class GameSystem : MonoBehaviour, IGameSystem
    {
        [Space]
        [SerializeField]
        private List<MonoBehaviour> gameGroups = new List<MonoBehaviour>();

        [SerializeField]
        private bool isAutoLaunch;

        private GameSystemContext systemContext;
        public GameState GameState { get; private set; }

        private void Awake()
        {
            systemContext = new GameSystemContext();
            Logger.WriteLog($"Загружена: {SceneManager.GetActiveScene().name}");
        }
        private void Start()
        {
            AutoLoadGroupsInGameSystem();           
            systemContext.LoadElements(gameGroups);

            if (isAutoLaunch)
            {
                InitGame();
                ReadyGame();
                StartGame();
            }
        }

        private void AutoLoadGroupsInGameSystem()
        {
            var groups = FindObjectsOfType<GameElementsGroup>();
            foreach (var group in groups)
            {
                if (group.gameObject.activeSelf && group.IsAutoLoading)
                    AddElementsGroup(group);
            }
        }
        public void AddElementsGroup(MonoBehaviour group)
        {
            if (!gameGroups.Contains(group))
                gameGroups.Add(group);
            else
                throw new Exception("douple adding in system!");
        }

        public void InitGame()
        {
            GameState = GameState.INITIALIZED;
            systemContext.InitGame(this);
        }

        public void FinishGame()
        {
            GameState = GameState.READY;
            systemContext.FinishGame();
        }

        public void PauseGame()
        {
            GameState = GameState.PAUSED;
            systemContext.PauseGame();           
        }

        public void ResumeGame()
        {
            GameState = GameState.PLAYING;
            systemContext.ResumeGame();
        }

        public void StartGame()
        {
            GameState = GameState.PLAYING;
            systemContext.StartGame();
            Time.timeScale = 1;
        }

        public void ReadyGame()
        {
            GameState = GameState.READY;
            systemContext.ReadyGame();
        }

        public void ChangeScene()
        {
            systemContext.ChangeScene();
        }

     
    }
}
