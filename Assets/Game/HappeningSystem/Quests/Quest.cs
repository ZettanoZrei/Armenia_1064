using Model.Entities.Persons;
using System.ComponentModel;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    [Serializable]
    public class Quest
    {
        public string Title { get; set; }
        public string Pointer { get; set; }
        public bool IsCampQuest { get; set; }
        public PersonName PersonName { get; set; }
        public bool IsRequired { get; set; }

        public Quest() { }
        public Quest(bool IsRequired, string title, string pointer, bool isCampQuest, PersonName personName)
        {
            this.Title = title;
            this.Pointer = pointer;
            this.IsCampQuest = isCampQuest;
            this.PersonName = personName;
            this.IsRequired = IsRequired;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Quest quest)) return false;

            return Title == quest.Title && Pointer == quest.Pointer && PersonName.Name == quest.PersonName.Name;
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }
    }
}
