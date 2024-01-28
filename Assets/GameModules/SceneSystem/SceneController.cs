using UnityEngine;
using Zenject;

namespace Assets.Modules
{
    public class SceneController : MonoBehaviour, ILateDisposable
    {
        private  SceneManager gameManager;

        [Inject]
        public void Construct(SceneManager gameManager)
        {
            this.gameManager = gameManager;
        }

        private void Start()
        {
            gameManager.InitGame();
            gameManager.ReadyGame();
            gameManager.StartGame();
        }
        
        void ILateDisposable.LateDispose()
        {
            gameManager.EndGame();
        }
    }
}
