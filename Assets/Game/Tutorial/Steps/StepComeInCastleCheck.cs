using Assets.Game.Core;
using Assets.Game.Tutorial.Core;
using Assets.GameEngine;
using System;

namespace Assets.Game.Tutorial.Steps
{
    //проверяет заход в замок чтобы дать команду для активации тутора в первом диалоге
    class StepComeInCastleCheck : TutorialStep, ILeaveGameComponentDI
    {
        private SetupCampManager setupCampManager;

        public StepComeInCastleCheck(SetupCampManager setupCampManager, GameSystemDIController zenjectGameSystem)
        {
            this.setupCampManager = setupCampManager;
            stepType = TutorialStepType.CheckCastle;
            zenjectGameSystem.AddComponent(this);
        }

        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<TutorialStepType>> OnLaunchStep;

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

        void ILeaveGameComponentDI.LeaveGame()
        {
            setupCampManager.OnSetupCamp_Before -= Finish;
        }
    }
}
