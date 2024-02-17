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

        [Inject]
        public void Construct(SceneScriptManager manager)
        {
            this.sceneManager = manager;
        }

        private void Start()
        {
            sceneManager.InitScene();
            sceneManager.ReadyScene();
            sceneManager.StartScene();
        }

        void ILateDisposable.LateDispose()
        {
            if (sceneManager.GameState != SceneState.Finished)
                sceneManager.FinishScene();
        }
    }
}
