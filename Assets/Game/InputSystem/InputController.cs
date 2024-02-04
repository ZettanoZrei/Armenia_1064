using Assets.Game.Menu;
using Assets.Modules;
using Assets.Modules.UI;
using GameSystems.Modules;
using UnityEngine;
using Zenject;

namespace Assets.Game.InputSystem
{
    internal class InputController : IInitializable, 
        ISceneReady, 
        ISceneFinish
    {
        private SimpleButton menu;
        private SimpleButton diary;
        private SimpleButton map;

        private MenuManager menuManager;
        private readonly SignalBus signalBus;

        public InputController(MenuManager menuManager, SignalBus signalBus, [Inject(Id = "menu")] SimpleButton menu)
        {
            this.menuManager = menuManager;
            this.signalBus = signalBus;
            this.menu = menu;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }

        void ISceneReady.ReadyScene()
        {
            menu.OnClick += OpenMenu;
        }
        void ISceneFinish.FinishScene()
        {
            menu.OnClick -= OpenMenu;
        }
        private void OpenMenu()
        {
            menuManager.ShowMenu();
        }
    }
}
