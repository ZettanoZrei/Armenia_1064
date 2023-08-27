using Assets.Game.HappeningSystem;
using Model.Entities.Answers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Assets.Save
{
    //this is camp dialogs
    public class BSRepositoryCampQuestTrigger
    {
        private readonly Dictionary<Scene, Dictionary<int, CampQuestTriggerInfo>> triggers = new Dictionary<Scene, Dictionary<int, CampQuestTriggerInfo>>();
        public IReadOnlyDictionary<Scene, Dictionary<int, CampQuestTriggerInfo>> Triggers => triggers;

        public void Add(Scene scene, Dictionary<int, CampQuestTriggerInfo> info)
        {
            triggers[scene] = info;
        }


        public bool TryGetData(Scene scene, out Dictionary<int, CampQuestTriggerInfo> info)
        {
            if (triggers.ContainsKey(scene))
            {
                info = triggers[scene];
                return true;
            }
            info = new Dictionary<int, CampQuestTriggerInfo>();
            return false;
        }
        //public bool TryGetData(int index, out bool isDone, out List<SingleQuestConsequences> fastPointers)
        //{
        //    if (triggers.ContainsKey(index))
        //    {
        //        isDone = triggers[index].isDone;
        //        fastPointers = triggers[index].fastPointers;
        //        return true;
        //    }
        //    isDone = default;
        //    fastPointers = new List<SingleQuestConsequences>();
        //    return false;
        //}

        public void Clear()
        {
            if (this.triggers != null)
                triggers.Clear();
        }
    }


    [Serializable]
    public class CampQuestTriggerInfo
    {
        public bool isDone;
        public List<SingleQuestConsequences> fastPointers = new List<SingleQuestConsequences>();

        public CampQuestTriggerInfo() { }
        public CampQuestTriggerInfo(bool isDone, List<SingleQuestConsequences> fastPointers)
        {
            this.isDone = isDone;
            this.fastPointers = fastPointers;
        }
    }
}
