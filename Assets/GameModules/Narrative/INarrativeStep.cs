using System;

namespace Assets.Game.Core
{
    public interface INarrativeStep<T>
    {
        event Action OnFinishStep;
        event Action<INarrativeStep<T>> OnLaunchStep;
        T StepType { get; }
        void Begin();
        void Finish();
    }
}
