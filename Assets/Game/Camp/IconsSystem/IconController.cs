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
    public class IconController : IInitializable, ISceneReady, ISceneInitialize
    {
        private readonly IconsFabrica iconsManager;
        private readonly CampBackgroundManager campBackgroundManager;
        private readonly CampIncomingData incomingData;
        private readonly SignalBus signalBus;
        private List<CampIcon> icons;

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

        void ISceneInitialize.InitScene()
        {
            IEnumerable<Quest> availableCampQuests = incomingData.DialogAvailable > 0 ? incomingData.CampQuests.OrderByDescending(x => x.IsRequired)
    : incomingData.CampQuests.Where(x => x.IsRequired);

            Logger.WriteLog($"availableCampQuests: {availableCampQuests.Count()}");

            icons = iconsManager.GreateCampQuests(availableCampQuests);
        }
        void ISceneReady.ReadyScene()
        {
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
