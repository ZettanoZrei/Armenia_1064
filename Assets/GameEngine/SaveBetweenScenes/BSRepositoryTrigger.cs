using Assets.Game.HappeningSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Save
{
    public class BSRepositoryTrigger
    {
        private readonly Dictionary<Scene, Dictionary<int, bool>> triggers = new Dictionary<Scene, Dictionary<int, bool>>();

        public IReadOnlyDictionary<Scene, Dictionary<int, bool>> Triggers => triggers;

        public void Add(Scene scene, Dictionary<int, bool> triger)
        {
            if (triggers.ContainsKey(scene))
            {
                foreach (var t in triger)
                    triggers[scene][t.Key] = t.Value;
            }
            else
            {
                triggers[scene] = triger;
            }
        }



        public bool TryGetData(Scene scene, out Dictionary<int, bool> trigger)
        {
            if (triggers.ContainsKey(scene))
            {
                trigger = triggers[scene];
                return true;
            }
            trigger = new Dictionary<int, bool>();
            return false;
        }


        public void Clear()
        {
            if (this.triggers != null)
                triggers.Clear();
        }

    }
}
