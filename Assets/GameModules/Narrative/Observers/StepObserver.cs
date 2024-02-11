using Assets.Game.Core;
using Assets.Game.Plot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.GameModules.Narrative
{
    abstract class StepObserver<StepType> : MonoBehaviour, IInitializable, ILateDisposable
    {
        [SerializeField] protected List<StepObserverCondition<StepType>> actions;

        protected StepManager<StepType> manager;

        void IInitializable.Initialize()
        {
            if (manager.IsActive)
                manager.OnShowStep += CheckStep;
        }
        void ILateDisposable.LateDispose()
        {
            manager.OnShowStep -= CheckStep;
        }

        private void Start()
        {
            CheckStepType(manager.LastShownStep);
        }
        private void CheckStep(IStep<StepType> step)
        {
            CheckStepType(step.StepType);
        }

        private void CheckStepType(StepType stepType)
        {
            if (!manager.IsActive)
                return;
           
            foreach (var action in actions)
            {
                //Debug.Log($"expected: {action.ExpectedStep} {Convert.ToInt32(action.ExpectedStep)} -- current: {stepType} {Convert.ToInt32(stepType)} ");
                switch (action.ExecutingCondition)
                {
                    case ExecutingCondition.Before:
                        if (Convert.ToInt32(action.ExpectedStep) > Convert.ToInt32(stepType))
                            action.Action.Execute();
                        break;
                    case ExecutingCondition.Equal:
                        if (Convert.ToInt32(action.ExpectedStep) == Convert.ToInt32(stepType))
                            action.Action.Execute();
                        break;
                    case ExecutingCondition.After:
                        if (Convert.ToInt32(action.ExpectedStep) < Convert.ToInt32(stepType))
                            action.Action.Execute();
                        break;
                }
            }
        }
    }
}
