using Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Model.Entities.Persons;

namespace Assets.Game.HappeningSystem
{
    public class DialogPersonFabrica : MonoBehaviour
    {
        [SerializeField]
        private DialogPersonPackCatalog personCatalog;

        [SerializeField]
        private Transform container;

        [SerializeField]
        private PersonFigureEntity prefab;

        [SerializeField]
        private UnityEvent<PersonFigureEntity> onCreatePersonView;

        public event Action<PersonFigureEntity> OnCreatePersonView;

        public void CreatePersonFigures(IEnumerable<Person> names)
        {
            foreach (var name in names)
                CreatePersonFigure(name);
        }
        public void CreatePersonFigure(Person person)
        {
            foreach (var pack in personCatalog)
            {
                if (pack.Name == person.Name.Name)
                {
                    var personView = Instantiate(prefab, container);
                    var initComponent = personView.Element<PersonModelInitComponent>();
                    initComponent.Init(person, pack);
                    onCreatePersonView?.Invoke(personView);
                    OnCreatePersonView?.Invoke(personView);
                    return;
                }
            }
            Logger.WriteLog($"can't find personPack {person.Name.Name}");
            throw new Exception($"can't find personPack {person.Name.Name}");
        }
    }
}
