using Assets.Game.Core;
using Assets.Game.HappeningSystem;
using Assets.Game.Plot.Core;
using System;

namespace Assets.Game.Plot.Steps
{
    class PStep11ChangePersonSprites : PlotStep
    {
        public override event Action OnFinishStep;
        public override event Action<IStep<PlotStepType>> OnLaunchStep;

        private readonly DialogPersonPackCatalog personPacks;
        public PStep11ChangePersonSprites(DialogPersonPackCatalog personPacks)
        {
            this.stepType = PlotStepType.ChangePeopleSprites;
            this.personPacks = personPacks;
        }

        public override void Begin()
        {
            foreach (var pack in personPacks)
            {
                pack.complectIndex = 1;
                pack.portraitIndex = 1;
            }
            OnLaunchStep?.Invoke(this);
            Finish();
        }

        public override void Finish()
        {
            OnFinishStep?.Invoke();
        }
    }
}
