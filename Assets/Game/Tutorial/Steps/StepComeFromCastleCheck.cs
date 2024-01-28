using Assets.Game.Core;
using Assets.Game.Tutorial.Core;
using Assets.GameEngine;
using Assets.Modules;
using GameSystems.Modules;
using System;
using Zenject;

namespace Assets.Game.Tutorial.Steps
{
    class StepComeFromCastleCheck : TutorialStep, IInitializable, IGameFinishElement
    {
        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<TutorialStepType>> OnLaunchStep;

        private MySceneManager sceneManager;
        private readonly SignalBus signalBus;

        public StepComeFromCastleCheck(MySceneManager sceneManager, SignalBus signalBus)
        {
            this.sceneManager = sceneManager;
            this.signalBus = signalBus;
            stepType = TutorialStepType.CheckCastleLeave;
        }
        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        void IGameFinishElement.FinishGame()
        {
            sceneManager.OnChangeScene_Post -= CheckTravel_1Scene;
        }
        public override void Begin()
        {
            sceneManager.OnChangeScene_Post += CheckTravel_1Scene;
            CheckTravel_1Scene(sceneManager.CurrentScene); //костыль
        }
        public override void Finish()
        {
            sceneManager.OnChangeScene_Post -= CheckTravel_1Scene;
            OnLaunchStep?.Invoke(this);
            OnFinishStep?.Invoke();
        }
        private void CheckTravel_1Scene(Scene scene)
        {
            if (scene == Scene.Travel_1)
            {
                Finish();
            }
        }
             
    }
}
