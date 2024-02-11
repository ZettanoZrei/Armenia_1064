namespace Assets.Game.Core
{
    public interface IStepConfig
    {
        bool Activate { get; }
        int StartStep { get; }
    }
}
