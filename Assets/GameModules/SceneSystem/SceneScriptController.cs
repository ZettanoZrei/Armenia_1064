using Assets.DialogSystem.Scripts;
using GameSystems;
using System;
using UnityEngine;
using VSCodeEditor;
using Zenject;

namespace Assets.Modules
{
    public class SceneScriptController : MonoBehaviour, ILateDisposable
    {
        private SceneScriptManager sceneManager;
        private ActorEventObserver actorEventObserver;

        [Inject]
        public void Construct(SceneScriptManager manager,[Inject(Optional =true)] ActorEventObserver actorEventObserver)
        {
            this.sceneManager = manager;
            this.actorEventObserver = actorEventObserver;
        }

        private void Start()
        {
            sceneManager.InitScene();
            sceneManager.ReadyScene();
            sceneManager.StartScene();
        }
        private void OnEnable()
        {
            if (actorEventObserver != null)
            {
                actorEventObserver.DialogStartEvent += PauseGame;
                actorEventObserver.DialogEndEvent += ResumeGame;
            }

        }

        private void OnDisable()
        {
            if (actorEventObserver != null)
            {
                actorEventObserver.DialogStartEvent -= PauseGame;
                actorEventObserver.DialogEndEvent -= ResumeGame;
            }
        }
        public void PauseGame()
        {
            sceneManager.PauseGame();
        }

        public void ResumeGame()
        {
            sceneManager.ResumeGame();
        }
        void ILateDisposable.LateDispose()
        {
            if (sceneManager.GameState != SceneState.Finished)
                sceneManager.FinishScene();
        }
    }
}
