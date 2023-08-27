using Entities;
using Model.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game
{
    internal class WorldState : MonoBehaviour //todo убрать
    {
        public static WorldState Instance { get; private set; }
        public PersonName Hero { get; private set; } = new PersonName("Барсег");

        private void Awake()
        {
            Instance = this;
        }
    }
}
