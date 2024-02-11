using Assets.Game.Camp;
using Assets.Modules;
using GameSystems;
using Model.Entities.Answers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Menu
{

    internal class MenuModel
    {
        private readonly MySceneManager sceneManager;
        private readonly SceneScriptManager scriptManager;
        public MenuModel(MySceneManager sceneManager, SceneScriptManager scriptManager)
        {
            this.sceneManager = sceneManager;
            this.scriptManager = scriptManager;
        }

        public List<Answer> GetMenuOptions()
        {
            return new List<Answer>
            {
                 new Answer { Index = 0, Text = "Продолжить" },
                 new Answer { Index = 1, Text = "Настройки" },
                 new Answer { Index = 2, Text = "В главное меню" },
                 new Answer { Index = 3, Text = "Выход из игры" },                 
            };
        }

        public void InvokeMenuOption(int? index)
        {
            switch(index)
            {
                case 0:
                    Continue();
                    break;
                case 1:
                    GoToSettings();
                    break;
                case 2:
                    GoToMainMenu();
                    break;
                case 3:
                    QuitTheGame();
                    break;
                default:break;
            }
        }

        private void QuitTheGame()
        {
            Application.Quit();
        }

        private void GoToMainMenu()
        {
            sceneManager.LoadMainMenuScene();
        }

        private void GoToSettings()
        {
            sceneManager.LoadSettings();
        }

        private void Continue()
        {
            scriptManager.ResumeGame();
            //Time.timeScale = 1;
        }

        public void Pause()
        {
            scriptManager.PauseGame();
            //Time.timeScale = 0;
        }
    }
}
