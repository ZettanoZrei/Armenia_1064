using Assets.GameModules.Narrative;
using Zenject;

namespace Assets.Game.Plot.Core
{
    class PlotObserver2 : NarrativeObserver<PlotStepType>
    {
        [Inject]
        public void Construct(PlotManager manager)
        {
            this.manager = manager;
        }
    }
}
