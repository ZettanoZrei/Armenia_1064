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
    class StepComeInCastleCheck : TutorialStep, IGameLeave
    {
        private SetupCampManager setupCampManager;
        public override event Action OnFinishStep;
        public override event Action<IStep<TutorialStepType>> OnLaunchStep;
        public StepComeInCastleCheck(SetupCampManager setupCampManager)
        {
            this.setupCampManager = setupCampManager;
            stepType = TutorialStepType.CheckCastle;
        }

        void IGameLeave.LeaveGame()
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
