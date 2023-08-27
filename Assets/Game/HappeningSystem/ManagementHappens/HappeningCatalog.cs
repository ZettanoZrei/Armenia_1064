using GameSystems;
using Model.Entities.Happenings;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Assets.Game.HappeningSystem
{
    public class HappeningCatalog : IEnumerable<Happening>
    {
        private List<Happening> happenings = new List<Happening>();

        public void AddHappening(Happening happening)
        {
            if(!happenings.Contains(happening))
                happenings.Add(happening);  
        }
        public IEnumerator<Happening> GetEnumerator()
        {
            foreach (var happen in happenings)
                yield return happen;
        }

        public Happening GetHappening(string happeningName)
        {
            return happenings.First(x => x.Title == happeningName);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
