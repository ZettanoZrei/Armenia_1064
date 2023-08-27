using Model.Entities.Persons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.HappeningSystem
{
    public class FigurePlaceFactory : MonoBehaviour, IEnumerable<DialogBaseFigurePlace>
    {
        [SerializeField] private Transform firstLine;
        [SerializeField] private Transform secondLine;
        [SerializeField] private DialogBaseFigurePlace basePlacePrefab; //todo: add by config
        [SerializeField] private DialogMainFigurePlace mainPlacePrefab;
        [SerializeField] private Collider2D firstLneBound; 
        [SerializeField] private Collider2D secondLineBound; 

        private List<DialogMainFigurePlace> firstLinePersonPlaces = new List<DialogMainFigurePlace>();
        public DialogMainFigurePlace CreatePlace(int xPosition)
        {
            var mainPlace = Instantiate(mainPlacePrefab, firstLine);
            firstLinePersonPlaces.Add(mainPlace);
            var namePart = firstLinePersonPlaces.IndexOf(mainPlace);
            mainPlace.SetPosition(xPosition).SetBound(firstLneBound).SetName(namePart);
            var subPlace = Instantiate(basePlacePrefab, secondLine);
            subPlace.SetPosition(xPosition).SetBound(secondLineBound).SetName(namePart);
            mainPlace.SecondLinePlace = subPlace;

            return mainPlace;
        }

        public DialogMainFigurePlace GetLinePlaces(PersonName name)
        {
            return firstLinePersonPlaces.First(x => x.PersonName.Equals(name));
        }
        public IEnumerator<DialogBaseFigurePlace> GetEnumerator()
        {
            foreach (var place in firstLinePersonPlaces)
            {
                yield return place;
                yield return place.SecondLinePlace;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
