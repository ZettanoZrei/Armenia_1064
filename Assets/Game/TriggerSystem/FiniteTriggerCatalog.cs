using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Game.HappeningSystem
{
    public class FiniteTriggerCatalog : MonoBehaviour
    {
        [SerializeField] private StaticTriggerComposite[] compositeTriggers;
        private int index = 0;

        private void Awake()
        {
            SetIndex();
        }

        public IEnumerable<T> GetElements<T>() where T : BaseFiniteTrigger
        {
            var list = new List<T>();
            foreach (var composite in compositeTriggers)
            {
                foreach (var trigger in composite)
                {
                    if (trigger is T result)
                    {                       
                        list.Add(result);
                    }
                }
            }
            return list;
        }
        private void SetIndex()
        {
            foreach (var composite in compositeTriggers)
            {
                foreach (var trigger in composite)
                {
                    trigger.Index = index++;
                }
            }
        }
    }
}
