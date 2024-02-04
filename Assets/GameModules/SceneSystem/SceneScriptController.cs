using GameSystems;
using UnityEngine;
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
            sceneManager.InitGame();
            sceneManager.ReadyGame();
            sceneManager.StartGame();
        }

        void ILateDisposable.LateDispose()
        {
            if (sceneManager.GameState != SceneState.Finished)
                sceneManager.EndGame();
        }
    }
}
