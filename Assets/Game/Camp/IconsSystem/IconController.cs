using Assets.Game.Camp.Background;
using Assets.Game.HappeningSystem;
using GameSystems;
using Model.Entities.Persons;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Game.Camp.IconsSystem
{
    public class IconController : MonoBehaviour, IGameInitElement
    {
        private IconsFabrica iconsManager;
        private CampBackgroundManager campBackgroundManager;
        private CampIncomingData incomingData;


        [Inject]
        public void Construct(IconsFabrica iconsManager, CampBackgroundManager campBackgroundManager, CampIncomingData incomingData)
        {
            this.iconsManager = iconsManager;
            this.campBackgroundManager = campBackgroundManager;
            this.incomingData = incomingData;
        }

        void IGameInitElement.InitGame(IGameSystem _)
        {
            IEnumerable<Quest> availableCampQuests = incomingData.DialogAvailable > 0 ? incomingData.CampQuests.OrderByDescending(x => x.IsRequired)
                : incomingData.CampQuests.Where(x => x.IsRequired);

            Logger.WriteLog($"availableCampQuests: {availableCampQuests.Count()}");

            var icons = iconsManager.GreateCampQuests(availableCampQuests);
            var iconGenerator = new IconsFieldAllocator();
            iconGenerator.AllocateIcons(icons, campBackgroundManager.CampBackground.Fields);

            foreach (var icon in icons.Where(x => !x.IsRequired)) 
            {
                icon.OnIconClick += MinusAvailableDialogs;
            }
        }

        private void MinusAvailableDialogs(string quest)
        {
            incomingData.MinusDialogAvailable(1);
            incomingData.MinusCampDialog(quest);
        }
    }
}
