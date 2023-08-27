using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.HappeningSystem
{
    class StaticTriggerComposite : BaseFiniteTrigger, IEnumerable<BaseFiniteTrigger>
    {
        [SerializeField] private List<BaseFiniteTrigger> baseFiniteTriggers;

        public IEnumerator<BaseFiniteTrigger> GetEnumerator()
        {
            return ((IEnumerable<BaseFiniteTrigger>)baseFiniteTriggers).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)baseFiniteTriggers).GetEnumerator();
        }
    }
}
