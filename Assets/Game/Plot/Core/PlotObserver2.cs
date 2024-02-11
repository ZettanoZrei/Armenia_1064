using Assets.GameModules.Narrative;
using Zenject;

namespace Assets.Game.Plot.Core
{
    class PlotObserver2 : StepObserver<PlotStepType>
    {
        [Inject]
        public void Construct(PlotManager manager)
        {
            this.manager = manager;
        }
    }
}
