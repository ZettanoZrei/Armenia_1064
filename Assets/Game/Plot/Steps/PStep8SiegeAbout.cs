using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.HappeningSystem;
using Assets.Game.Plot.Core;
using Assets.Game.Plot.UI;
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
    //8
    class PStep8SiegeAbout : PlotStep, IInitializable, IGameFinishElement
    {
        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;

        private readonly HappeningManager happeningManager;
        private readonly SignalBus signalBus;

        public PStep8SiegeAbout(HappeningManager happeningManager, SignalBus signalBus)
        {
            this.happeningManager = happeningManager;
            this.signalBus = signalBus;
            this.stepType = PlotStepType.SiegeAbout;
        }
        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        void IGameFinishElement.FinishGame()
        {
            happeningManager.OnFinishHappening -= DoFinsih;
        }
        public override async void Begin()
        {
            await Task.Delay(1000);
            happeningManager.LaunchHappenWithoutQueue("Аревберд_Штурм"); 
            happeningManager.OnFinishHappening += DoFinsih;
            OnLaunchStep?.Invoke(this);
        }
        private void DoFinsih(Happening _)
        {
            Finish();
        }
        public override void Finish()
        {
            happeningManager.OnFinishHappening -= DoFinsih;
            OnFinishStep?.Invoke();
        }        
    }
}
