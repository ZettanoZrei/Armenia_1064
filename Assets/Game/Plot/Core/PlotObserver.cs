using Assets.Game.Core;
using UnityEngine;
using Zenject;

namespace Assets.Game.Plot.Core
{
    abstract class PlotObserver : MonoBehaviour, IInitializable, ILateDisposable
    {
        [SerializeField]
        protected PlotStepType expectedStep;

        protected PlotManager plotManager;

        [Inject]
        public void Construct(PlotManager plotlManager)
        {
            this.plotManager = plotlManager;
        }
        void IInitializable.Initialize()
        {
            plotManager.OnShowStep += CheckStep;
        }
        void ILateDisposable.LateDispose()
        {
            plotManager.OnShowStep -= CheckStep;
        }
        private void Start()
        {
            if (plotManager.IsActive && expectedStep > plotManager.LastShownStep)
            {
                DoBeforeStep();
            }
        }

        protected void CheckStep(INarrativeStep<PlotStepType> step)
        {
            if (!plotManager.IsActive) return;

            if (step.StepType == expectedStep)
            {
                DoAfterStep();
            }
        }
        protected abstract void DoAfterStep();
        protected abstract void DoBeforeStep();
    }
}
