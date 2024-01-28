using Assets.Game.Camp;
using Assets.Game.Core;
using Assets.Game.HappeningSystem;
using Assets.Game.Plot.Core;
using Assets.GameEngine;
using Assets.Modules;
using GameSystems.Modules;
using System;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Game.Plot.Steps
{
    //5.1
    class PStep5BeginAttack_1 : PlotStep, IInitializable, IGameFinishElement
    {
        private readonly CampIncomingData incomingData;
        private readonly HappeningManager happeningManager;
        private readonly SignalBus signalBus;

        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;
        public PStep5BeginAttack_1(CampIncomingData incomingData, HappeningManager happeningManager, SignalBus signalBus)
        {
            this.incomingData = incomingData;
            this.happeningManager = happeningManager;
            this.signalBus = signalBus;
            this.stepType = PlotStepType.BeginAttack_1;
        }

        void IGameFinishElement.FinishGame()
        {
            incomingData.OnDialogAvailableChange -= CheckIfAllDialogFinished;
            happeningManager.OnFinishHappeningAsync -= DoFinish;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        public override void Begin()
        {
            incomingData.OnDialogAvailableChange += CheckIfAllDialogFinished;
        }
        private void CheckIfAllDialogFinished(int value)
        {
            if (value <= 0)
            {
                incomingData.OnDialogAvailableChange -= CheckIfAllDialogFinished;
                happeningManager.OnFinishHappeningAsync += DoFinish;
            }
        }

        private Task DoFinish()
        {
            happeningManager.OnFinishHappeningAsync -= DoFinish;
            Finish();
            return Task.CompletedTask;
        }

        public override void Finish()
        {
            OnFinishStep?.Invoke();
        }

        
    }
}
