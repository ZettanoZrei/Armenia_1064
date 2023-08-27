using Zenject;

namespace Assets.Game.Menu
{
    class MenuManager 
    {
        private readonly MenuModel menuModel;
        private readonly PopupManager popupManager;
        private bool isActive;

        public bool IsActive => isActive; 

        public MenuManager(MenuModel menuModel, PopupManager popupManager)
        {
            this.menuModel = menuModel;
            this.popupManager = popupManager;
        }

        public void ShowMenu()
        {
            if(isActive) return;

            menuModel.Pause();
            var menuPopup = popupManager.ShowPopup(PopupType.MenuPopup) as MenuView;
            menuPopup.OnDecitionMade += InvokeMenuOption;
            menuPopup.SetOption(menuModel.GetMenuOptions());
            isActive = true;
        }

        public void HideMenu()
        {
            if(!isActive) return;
            isActive = false;
            InvokeMenuOption(0);
        }


        private void InvokeMenuOption(int index)
        {
            popupManager.ClosePopup(PopupType.MenuPopup);
            menuModel.InvokeMenuOption(index);
            isActive = false;
        }
    }
}
