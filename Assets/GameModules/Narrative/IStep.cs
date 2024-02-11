using System;

namespace Assets.Game.Core
{
    public interface IStep<T>
    {
        event Action OnFinishStep;
        event Action<IStep<T>> OnLaunchStep;
        T StepType { get; }
        void Begin();
        void Finish();
    }
}
