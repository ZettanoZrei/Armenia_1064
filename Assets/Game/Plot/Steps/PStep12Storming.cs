using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.HappeningSystem;
using Assets.Game.Plot.Core;
using Assets.GameEngine;
using Entities;
using Model.Entities.Happenings;
using System;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Game.Plot.Steps
{
    //12
    class PStep12Storming : PlotStep, ILeaveGameComponentDI
    {
        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;
        private HappeningManager happeningManager;

        public PStep12Storming(HappeningManager happeningManager, GameSystemDIController zenjectGameSystem)
        {
            this.happeningManager = happeningManager;
            stepType = PlotStepType.Storming;
            zenjectGameSystem.AddComponent(this);
        }

       
        public override async void Begin()
        {
            await DoBegin();
        }

        private async Task DoBegin()
        {
            await Task.Delay(1000);
            OnLaunchStep?.Invoke(this);
            happeningManager.LaunchHappenWithoutQueue("Аревберд_Штурм"); 
            happeningManager.OnFinishHappening += HappenFinsihHandler;
        }

        private void HappenFinsihHandler(Happening happening)
        {
            if (happening.Title == "Аревберд_Штурм_Случай4") 
                Finish();
        }

        public override void Finish()
        {
            happeningManager.OnFinishHappening -= HappenFinsihHandler;
            OnFinishStep?.Invoke();
        }

        void ILeaveGameComponentDI.LeaveGame()
        {
            happeningManager.OnFinishHappening -= HappenFinsihHandler;
        }
    }
}
