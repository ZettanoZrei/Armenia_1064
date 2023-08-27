using Assets.Game.Plot.Core;
using Assets.Game.Tutorial.Core;

namespace Loader
{
    class NG_TaskBeginNarrative : ING_Task
    {
        private readonly PlotManager plotManager;
        private readonly TutorialManager tutorialManager;

        public NG_TaskBeginNarrative(PlotManager plotManager, TutorialManager tutorialManager)
        {
            this.plotManager = plotManager;
            this.tutorialManager = tutorialManager;
        }
        void ING_Task.Execute()
        {
            plotManager.Reset();
            tutorialManager.Reset();

            plotManager.Begin();
            tutorialManager.Begin();
        }
    }
}
