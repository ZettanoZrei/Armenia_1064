using Entities;
using Model.Entities.Persons;
using ModestTree.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Game.HappeningSystem
{
    public class DialogPersonKeeper : MonoBehaviour
    {
        private List<PersonFigureEntity> personModels = new List<PersonFigureEntity>();

        [SerializeField]
        private UnityEvent<PersonFigureEntity> onPersonAdded;

        public event Action<PersonFigureEntity> OnPersonAdded;
        public void AddPerson(PersonFigureEntity personView)
        {
            if (personModels.All(model => !personView.Equals(model)))
            {
                personModels.Add(personView);
                onPersonAdded?.Invoke(personView);
                OnPersonAdded?.Invoke(personView);
            }
        }

        public PersonFigureEntity GetPerson(PersonName personName)
        {
            return personModels.First(view => view.Element<PersonModelInfoComponent>().Name.Equals(personName));
        }
    }
}
