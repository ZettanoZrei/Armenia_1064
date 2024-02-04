using Assets.Game.Core;
using Assets.Game.Tutorial.Core;
using Assets.GameEngine;
using Assets.Modules;
using GameSystems.Modules;
using System;
using Zenject;

namespace Assets.Game.Tutorial.Steps
{
    //проверяет заход в замок чтобы дать команду для активации тутора в первом диалоге
    class StepComeInCastleCheck : TutorialStep, IInitializable, ISceneFinish
    {
        private SetupCampManager setupCampManager;
        private readonly SignalBus signalBus;
        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<TutorialStepType>> OnLaunchStep;
        public StepComeInCastleCheck(SetupCampManager setupCampManager, SignalBus signalBus)
        {
            this.setupCampManager = setupCampManager;
            this.signalBus = signalBus;
            stepType = TutorialStepType.CheckCastle;
        }
        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        void ISceneFinish.FinishScene()
        {
            setupCampManager.OnSetupCamp_Before -= Finish;
        }
        

        public override void Begin()
        {
            setupCampManager.OnSetupCamp_Before += Finish;
            OnLaunchStep?.Invoke(this);
        }

        public override void Finish()
        {
            setupCampManager.OnSetupCamp_Before -= Finish;
            OnFinishStep?.Invoke();
        }
           
    }
}
