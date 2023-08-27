using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Model.Entities.Persons;
using System.Linq;
using Model.Types;

namespace Assets.Game.HappeningSystem
{
    //public class DialogPlaceCatalog : MonoBehaviour, IEnumerable<DialogBaseFigurePlace>
    //{
    //    [SerializeField]
    //    private List<DialogMainFigurePlace> firstLinePersonPlaces = new List<DialogMainFigurePlace>();

    //    public DialogMainFigurePlace GetLinePlaces(PersonName name)
    //    {
    //        return firstLinePersonPlaces.First(x => x.PersonName.Equals(name));
    //    }

    //    public DialogMainFigurePlace GetFreeLinePlaces(PositionType positionType)
    //    {
    //        return firstLinePersonPlaces
    //            .Where(x => x.PositionType == positionType)
    //            .Where(x => !x.IsBusy)
    //            .First();
    //    }

    //    public IEnumerator<DialogBaseFigurePlace> GetEnumerator()
    //    {
    //        foreach (var place in firstLinePersonPlaces)
    //        {
    //            yield return place;
    //            yield return place.SecondLinePlace;
    //        }
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }
    //}
}
