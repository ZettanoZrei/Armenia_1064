using ExtraInjection;
using Zenject;

namespace Assets.Game.Menu
{
    class MenuManager : IExtraInject
    {
        private readonly MenuModel menuModel;
        [ExtraInject] private PopupManager popupManager;
        private bool isActive;

        public bool IsActive => isActive; 

        public MenuManager(MenuModel menuModel)
        {
            this.menuModel = menuModel;
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
