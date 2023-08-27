using Model.Entities.Persons;
using Model.Entities.Phrases;
using Model.Types;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    public class FigurePersonManager : MonoBehaviour
    {
        private DialogPersonAllocator dialogPersonAllocator;
        private DialogPersonFabrica dialogPersonFabrica;
        private DialogPersonFocus dialogPersonFocus;
        private DialogPersonKeeper dialogPersonKeeper;
        private FigurePlaceFactory figurePlaceFactory;

        [Inject]
        public void Construct(DialogPersonAllocator dialogPersonAllocator, DialogPersonFabrica dialogPersonFabrica,
            DialogPersonFocus dialogPersonFocus, DialogPersonKeeper dialogPersonKeeper, FigurePlaceFactory figurePlaceFactory)
        {
            this.dialogPersonAllocator = dialogPersonAllocator;
            this.dialogPersonFabrica = dialogPersonFabrica;
            this.dialogPersonFocus = dialogPersonFocus;
            this.dialogPersonKeeper = dialogPersonKeeper;
            this.figurePlaceFactory = figurePlaceFactory;
        }

        public event Action<LineView> OnSetLineView
        {
            add { dialogPersonFocus.OnSetLineView += value; }
            remove { dialogPersonFocus.OnSetLineView -= value; }
        }

        public void CreatePersonFigures(IEnumerable<Person> persons)
        {
            foreach (var person in persons)
                dialogPersonFabrica.CreatePersonFigure(person);
        }

        public void CreatePersonFigure(Person person)
        {
            dialogPersonFabrica.CreatePersonFigure(person);
        }
        public void Focus(PersonName personName, bool IsShowFace, LineView lineView)
        {
            dialogPersonFocus.Focus(personName, IsShowFace, lineView);
        }
        public void FocusInvert(PersonName personName, bool IsShowFace)
        {
            dialogPersonFocus.FocusInvert(personName, IsShowFace);
        }

        public void Clean()
        {
            foreach(var place in figurePlaceFactory)
            {
                if(place is DialogMainFigurePlace mainPlace)
                {
                    mainPlace.Clean();
                }
            }
        }

        private void OnEnable()
        {
            dialogPersonFabrica.OnCreatePersonView += dialogPersonKeeper.AddPerson;
            dialogPersonKeeper.OnPersonAdded += dialogPersonAllocator.Allocate;
        }

        private void OnDisable()
        {
            dialogPersonFabrica.OnCreatePersonView -= dialogPersonKeeper.AddPerson;
            dialogPersonKeeper.OnPersonAdded -= dialogPersonAllocator.Allocate;
        }
    }
}
