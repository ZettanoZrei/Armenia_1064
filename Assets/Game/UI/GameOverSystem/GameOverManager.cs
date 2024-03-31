using Assets.Game.HappeningSystem;
using Assets.Game.Parameters;
using ExtraInjection;
using Model.Entities.Happenings;

namespace Assets.Game.UI.FailGameSystem
{

    class GameOverManager : IExtraInject
    {
        [ExtraInject] private PopupManager popupManager;
        private readonly MySceneManager sceneManager;
        private GameOverPopup popup;
        
        public GameOverManager(MySceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        

        public void GameOver()
        {
            //if (happeningLauncher.IsHappeningActive)
            //    happeningLauncher.OnFinishHappening += ShowGameOver;
            //else
            //    ShowGameOver(null);
        }

        private void ShowGameOver(Happening _)
        {
            //happeningLauncher.OnFinishHappening -= ShowGameOver;
            popup = popupManager.ShowPopup(PopupType.GameOverPopup) as GameOverPopup;
            popup.OnClick += EndGame;
        }

        

        private void EndGame()
        {
            popup.OnClick -= EndGame;
            popupManager.ClosePopup(PopupType.EndGamePopup);
            sceneManager.LoadMainMenuScene();
        }
    }
}
