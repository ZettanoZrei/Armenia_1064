using System.Collections.Generic;
using UnityEngine;

namespace Assets.Save
{
    public class BSRepositoryCaravan
    {
        private Dictionary<Scene, List<CaravanData>> positionData = new Dictionary<Scene, List<CaravanData>>();

        public IReadOnlyDictionary<Scene, List<CaravanData>> PositionData  => positionData; 

        public void Add(Scene scene, List<CaravanData> data)
        {
            this.positionData[scene] = data;
        }

        public bool TryGetData(Scene scene, out List<CaravanData> data)
        {
            if (this.positionData.ContainsKey(scene))
            {
                data = this.positionData[scene];
            }
            else
                data = new List<CaravanData>();
            return data != null;
        }

        public void Clear()
        {
            if (this.positionData != null)
                positionData.Clear();
        }
    }
}
