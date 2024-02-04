using Assets.Game.Camp;
using Assets.Game.Camp.IconsSystem;
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
    //5.2
    class PStep5BeginAttack_2 : PlotStep, IInitializable, ISceneFinish
    {
        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;
        private readonly HappeningManager happeningManager;
        private readonly SignalBus signalBus;

        public PStep5BeginAttack_2(HappeningManager happeningManager, SignalBus signalBus)
        {
            this.happeningManager = happeningManager;
            this.signalBus = signalBus;
            this.stepType = PlotStepType.BeginAttack_2;
        }
        void ISceneFinish.FinishScene()
        {
            happeningManager.OnFinishHappening -= DoFinsih;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        public override void Begin()
        {
            DoBegin();
        }

        private async Task DoBegin()
        {
            happeningManager.OnFinishHappeningAsync -= DoBegin;
            await Task.Delay(TimeSpan.FromSeconds(1.5));
            OnLaunchStep?.Invoke(this);
            happeningManager.LaunchHappenWithoutQueue("Аревберд Нападение");
            happeningManager.OnFinishHappening += DoFinsih;
        }

        private void DoFinsih(Happening happening)
        {
            if (happening.Title == "Аревберд_Нападение_Диалог")
                Finish();
        }

        public override void Finish()
        {
            happeningManager.OnFinishHappening -= DoFinsih;
            OnFinishStep?.Invoke();
        }

        
    }
}
