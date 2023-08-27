using Assets.Game.HappeningSystem;
using Assets.Game.Parameters;
using Model.Entities.Happenings;

namespace Assets.Game.UI.FailGameSystem
{

    class GameOverManager 
    {
        private readonly PopupManager popupManager;
        private readonly MySceneManager sceneManager;
        private readonly HappeningLauncher happeningLauncher;
        private GameOverPopup popup;
        
        public GameOverManager(PopupManager popupManager, MySceneManager sceneManager, HappeningLauncher happeningLauncher)
        {
            this.popupManager = popupManager;
            this.sceneManager = sceneManager;
            this.happeningLauncher = happeningLauncher;
        }

        

        public void GameOver()
        {
            if (happeningLauncher.IsHappeningActive)
                happeningLauncher.OnFinishHappening += ShowGameOver;
            else
                ShowGameOver(null);
        }

        private void ShowGameOver(Happening _)
        {
            happeningLauncher.OnFinishHappening -= ShowGameOver;
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
