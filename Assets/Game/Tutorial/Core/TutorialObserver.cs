using Assets.Game.Core;
using Assets.Game.Tutorial.Core;
using UnityEngine;
using Zenject;

abstract class TutorialObserver : MonoBehaviour,  IInitializable, ILateDisposable
{
    [SerializeField]
    protected TutorialStepType expectedStep;

    protected TutorialManager tutorialManager;

    [Inject]
    public void Construct(TutorialManager tutorialManager)
    {
        this.tutorialManager = tutorialManager;
    }
    void IInitializable.Initialize()
    {
        tutorialManager.OnShowStep += CheckStep;
    }
    void ILateDisposable.LateDispose()
    {
        tutorialManager.OnShowStep -= CheckStep;
    }
    private void Start()
    {
        if (tutorialManager.IsActive && expectedStep > tutorialManager.LastShownStep)
        {
            DoBeforeStep();
        }
    }

    protected void CheckStep(INarrativeStep<TutorialStepType> step)
    {
        if (!tutorialManager.IsActive) return;
        Logger.WriteLog($"{gameObject.name}. await: {expectedStep}, get: {step.StepType}, result: {step.StepType >= expectedStep}");
        if (step.StepType >= expectedStep)
        {
            DoAfterStep();
        }
    }
    protected abstract void DoAfterStep();
    protected abstract void DoBeforeStep();    
}
