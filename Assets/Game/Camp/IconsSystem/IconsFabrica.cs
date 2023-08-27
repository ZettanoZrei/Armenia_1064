using Assets.Game.Camp.Background;
using Assets.Game.HappeningSystem;
using Model.Entities.Persons;
using Model.Entities.Quest;
using System;
using System.Collections.Generic;


namespace Assets.Game.Camp.IconsSystem
{
    public class IconsFabrica
    {
        private readonly CampIcon.Factory factory;
        private readonly DialogPersonPackCatalog personPackCatalog;
        private List<CampIcon> activeIcons;

        public IconsFabrica(CampIcon.Factory factory, DialogPersonPackCatalog personPackCatalog)
        {
            this.factory = factory;
            this.personPackCatalog = personPackCatalog;
        }


        public List<CampIcon> GreateCampQuests(IEnumerable<Quest> quests)
        {
            var icons = new List<CampIcon>();
            foreach (var questPack in quests)
            {
                var icon = factory.Create(questPack.IsRequired, questPack.Title);
                var sprite = personPackCatalog.GetPack(questPack.PersonName.Name).Portret;
                icon.SetPortrait(sprite, questPack.PersonName);
                icons.Add(icon);

                icon.OnClose += RemoveIconFromActive;
            }
            activeIcons = new List<CampIcon>(icons);
            return icons;
        }

        private void RemoveIconFromActive(CampIcon icon)
        {
            activeIcons.Remove(icon);
        }
    }
}
