using Assets.Game.Camp;
using Assets.Game.Core;
using Assets.Game.Plot.Core;
using Assets.GameEngine;
using Assets.Modules;
using GameSystems.Modules;
using System;
using Zenject;

namespace Assets.Game.Plot.Steps
{
    //10
    class PStep10ChangeCampSprite : PlotStep, IInitializable, IGameFinishElement
    {
        private readonly SetupCampManager campManager;
        private readonly CampIncomingData incomingData;
        private readonly MySceneManager sceneManager;
        private readonly SignalBus signalBus;

        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;

        public PStep10ChangeCampSprite(SetupCampManager campManager, CampIncomingData incomingData, MySceneManager sceneManager, SignalBus signalBus)
        {
            this.stepType = PlotStepType.ChangeCampSprite;
            this.campManager = campManager;
            this.incomingData = incomingData;
            this.sceneManager = sceneManager;
            this.signalBus = signalBus;
        }
        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }

        void IGameFinishElement.FinishGame()
        {
            sceneManager.OnChangeScene_Post -= DoFinish;
        }
        public override void Begin()
        {
            incomingData.CampImagePrefab = "АревбердВОгне";
            campManager.SetupCamp();

            sceneManager.OnChangeScene_Post += DoFinish;
            OnLaunchStep?.Invoke(this);
        }

        private void DoFinish(Scene _)
        {
            Finish();
        }

        public override void Finish()
        {
            sceneManager.OnChangeScene_Post -= DoFinish;
            OnFinishStep?.Invoke();
        }        
    }
}
