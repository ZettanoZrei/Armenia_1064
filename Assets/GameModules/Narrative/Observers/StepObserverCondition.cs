using Assets.Game.Plot.Core;
using System;
using UnityEngine;

namespace Assets.GameModules.Narrative
{
    [Serializable]
    class StepObserverCondition<StepType>
    {
        [SerializeField] protected ExecutingCondition executingCondition;
        [SerializeField] protected StepType expectedStep;
        [SerializeField] protected StepObserverAction action;
        public ExecutingCondition ExecutingCondition => executingCondition;
        public StepType ExpectedStep => expectedStep;
        public StepObserverAction Action => action;
    }
}
