using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.HappeningSystem;
using Assets.Game.Plot.Core;
using Assets.GameEngine;
using Assets.Modules;
using Entities;
using GameSystems.Modules;
using Model.Entities.Happenings;
using System;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Game.Plot.Steps
{
    //12
    class PStep12Storming : PlotStep, IInitializable, IGameFinishElement
    {
        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;
        private HappeningManager happeningManager;
        private readonly SignalBus signalBus;

        public PStep12Storming(HappeningManager happeningManager, SignalBus signalBus)
        {
            this.happeningManager = happeningManager;
            this.signalBus = signalBus;
            stepType = PlotStepType.Storming;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }

        public void FinishGame()
        {
            happeningManager.OnFinishHappening -= HappenFinsihHandler;
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

        
    }
}
