using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Game.Caravan.Save
{
    class SaveMechanics : MonoBehaviour
    {
        [SerializeField]
        private MoveBezierFollowHandler moveHandler;
        public List<CaravanData> GetData()
        {
            var list = new List<CaravanData>();
            foreach (var item in moveHandler.BezierFollows)
            {
                list.Add(new CaravanData {  Distance = item.Distance });
            }
            return list;
        }

        public void LoadData(List<CaravanData> caravanData)
        {
            for(int i = 0; i < caravanData.Count; i++)
            {
                moveHandler.BezierFollows[i].Distance = caravanData[i].Distance;
            }
        }

    }
}
