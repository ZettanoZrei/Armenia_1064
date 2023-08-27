using Assets.Game.Menu;
using Assets.Modules.UI;
using GameSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.InputSystem
{
    internal class InputController : MonoBehaviour, 
        IGameReadyElement, IGameFinishElement
    {
        [SerializeField] private SimpleButton menu;
        [SerializeField] private SimpleButton diary;
        [SerializeField] private SimpleButton map;

        private MenuManager menuManager;    

        [Inject]
        public void Constuct(MenuManager menuManager)
        {
            this.menuManager = menuManager;
        }

        void IGameReadyElement.ReadyGame()
        {
            menu.OnClick += OpenMenu;
        }
        void IGameFinishElement.FinishGame()
        {
            menu.OnClick -= OpenMenu;
        }
        private void OpenMenu()
        {
            menuManager.ShowMenu();
        }              
    }
}
