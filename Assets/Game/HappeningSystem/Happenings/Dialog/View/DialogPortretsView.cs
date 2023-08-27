using Model.Entities.Persons;
using Model.Entities.Phrases;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.HappeningSystem
{
    public class DialogPortretsView : MonoBehaviour
    {
        [SerializeField] private PortraitView hero;
        [SerializeField] private PortraitView person;

        [SerializeField]
        private DialogPersonPackCatalog personCatalog;

        private Dictionary<PersonName, (Sprite sprite, bool isMain)> portrets = new Dictionary<PersonName, (Sprite, bool)>();

        private void Awake()
        {
            CacheIcons();
        }

        public void SetPersonPortret(PersonName name, int relationValue)
        {
            if (name.Equals(WorldState.Instance.Hero))
            {                
                SetHeroPortret(false);
            }
            else
                SetAnotherPersonPortret(name, relationValue);
        }

        //ue Scroll View
        public void SetHeroPortret(bool leaveAnotherPortreit)
        {
            person.gameObject.SetActive(leaveAnotherPortreit);
            hero.gameObject.SetActive(true);
            hero.SetDialogPortrait(portrets[WorldState.Instance.Hero].sprite, false);
        }

        private void SetAnotherPersonPortret(PersonName name, int relationValue)
        {
            hero.gameObject.SetActive(false);
            person.gameObject.SetActive(true);
            person.SetDialogPortrait(portrets[name].sprite, portrets[name].isMain, name, relationValue);
        }

        private void CacheIcons()
        {
            foreach (DialogPersonPack pack in personCatalog)
            {
                var personName = new PersonName(pack.Name);
                if (!portrets.ContainsKey(personName))
                    portrets.Add(personName, (pack.Portret, pack.MainCharacter));
            }
        }
    }
}
