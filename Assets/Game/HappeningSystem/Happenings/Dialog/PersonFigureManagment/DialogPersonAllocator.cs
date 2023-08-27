using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
using Entities;
using System;
using Model.Types;
using Model.Entities.Persons;
using Zenject;
using Assets.Game.HappeningSystem.Happenings;

namespace Assets.Game.HappeningSystem
{
    public class DialogPersonAllocator : MonoBehaviour
    {
        [Inject] private FigurePlaceFactory figurePlaceFactory;
        private readonly List<PersonFigureEntity> cache = new List<PersonFigureEntity>();

        public void Allocate(PersonFigureEntity personView)
        {
            cache.Add(personView);
            var infoComponent = personView.Element<PersonModelInfoComponent>();
            var place = figurePlaceFactory.CreatePlace(infoComponent.XPosition);
            place.SetPerson(personView);
        }        
    }
}
