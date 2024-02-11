using Assets.Game.Configurations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Core
{
    public abstract class StepManager<IType>
    {
        protected readonly List<IStep<IType>> steps;
        protected IStepConfig config;
        public bool IsActive => config.Activate;

        private int currentStepIndex = 0;
        public int CurrentStepIndex => currentStepIndex;
        public IType LastShownStep { get; set; }

        public event Action<IStep<IType>> OnShowStep;
        public event Action OnComplete;
        public bool IsComplete { get; set; }
        public StepManager(List<IStep<IType>> steps)
        {
            this.steps = steps;
            foreach (var step in steps)
            {
                step.OnFinishStep += MoveStepIndex;
                step.OnFinishStep += MoveStep;
            }
        }

        public virtual void Begin()
        {
            currentStepIndex = config.StartStep;
            if (config.Activate)
            {
                MoveStep();
            }
        }
        protected virtual void Init()
        {
            foreach (var step in steps)
                step.OnLaunchStep += InvokeOnShowStep;
        }

        protected virtual void Finish()
        {
            foreach (var step in steps)
                step.OnLaunchStep -= InvokeOnShowStep;
        }

        protected void MoveStep()
        {
            if (currentStepIndex >= steps.Count || IsComplete)
                return;
           
            var step = steps[currentStepIndex];
            Logger.WriteLog($"{this.GetType().Name}. step - {step.StepType}");
            Debug.Log(step.StepType);

            step.Begin();            
        }

        private void MoveStepIndex()
        {
            currentStepIndex++;
            if (currentStepIndex == steps.Count)
            {
                IsComplete = true;
                OnComplete?.Invoke();
            }
        }

        protected void InvokeOnShowStep(IStep<IType> step)
        {
            LastShownStep = step.StepType;
            OnShowStep?.Invoke(step);
        }

        public void Reset()
        {
            currentStepIndex = 0;
        }
    }
}
