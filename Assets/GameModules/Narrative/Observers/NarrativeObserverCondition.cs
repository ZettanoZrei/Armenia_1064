using Assets.Game.Plot.Core;
using System;
using UnityEngine;

namespace Assets.GameModules.Narrative
{
    [Serializable]
    class NarrativeObserverCondition<StepType>
    {
        [SerializeField] protected ExecutingCondition executingCondition;
        [SerializeField] protected StepType expectedStep;
        [SerializeField] protected NarrativeObserverAction action;
        public ExecutingCondition ExecutingCondition => executingCondition;
        public StepType ExpectedStep => expectedStep;
        public NarrativeObserverAction Action => action;
    }
}
