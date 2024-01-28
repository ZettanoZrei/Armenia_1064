using Assets.Game.Camp.Background;
using Assets.Game.HappeningSystem;
using Assets.Modules;
using GameSystems.Modules;
using Model.Entities.Persons;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Game.Camp.IconsSystem
{
    public class IconController : IInitializable, IGameReadyElement
    {
        private readonly IconsFabrica iconsManager;
        private readonly CampBackgroundManager campBackgroundManager;
        private readonly CampIncomingData incomingData;
        private readonly SignalBus signalBus;

        public IconController(IconsFabrica iconsManager, CampBackgroundManager campBackgroundManager, CampIncomingData incomingData,
            SignalBus signalBus)
        {
            this.iconsManager = iconsManager;
            this.campBackgroundManager = campBackgroundManager;
            this.incomingData = incomingData;
            this.signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }

        void IGameReadyElement.ReadyGame()
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
