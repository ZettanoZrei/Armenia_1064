using Entities;
using Model.Entities.Persons;
using Model.Types;
using UnityEngine;

namespace Assets.Game.HappeningSystem
{
    class PersonModelInfoComponent : MonoBehaviour
    {
        public PersonName Name { get; private set; }
        public PositionType PositionType { get; private set; }
        public bool IsApproached { get; private set; }
        public int XPosition { get; private set; }
        public void GetInfo(Person person)
        {
            this.Name = person.Name;
            this.PositionType = person.PositionType;
            this.IsApproached = person.IsApproached;
            this.XPosition = person.XPosition;
        }
    }
}
