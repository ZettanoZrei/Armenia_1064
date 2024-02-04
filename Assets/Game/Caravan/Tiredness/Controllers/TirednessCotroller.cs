using Assets.Game.Parameters;
using Assets.Modules;
using Entities;
using GameSystems.Modules;
using System;
using UnityEngine;
using Zenject;

namespace Parameters
{
    class TirednessCotroller : IInitializable,
        ISceneInitialize, 
        ISceneReady, 
        ISceneFinish
    {
        private IMoveComponent moveComponent;
        private ITirednessComponent tirednessComponent;
        private IEntity caravan;
        private ParametersManager parametersManager;
        private readonly SignalBus signalBus;

        [Inject]
        public TirednessCotroller([Inject(Id = "caravan")] IEntity caravan, ParametersManager parametersManager, SignalBus signalBus)
        {
            this.caravan = caravan;
            this.parametersManager = parametersManager;
            this.signalBus = signalBus;
        }
        public void Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        void ISceneInitialize.InitScene()
        {
            moveComponent = this.caravan.Element<IMoveComponent>();
            tirednessComponent = this.caravan.Element<ITirednessComponent>();
        }


        void ISceneReady.ReadyScene()
        {
            tirednessComponent.OnDecreaseStamina += parametersManager.ChangeStamina;
            moveComponent.OnMovingEvent += StartTired;
            moveComponent.OnFinishMovingEvent += StopTired;
        }


        void ISceneFinish.FinishScene()
        {
            tirednessComponent.OnDecreaseStamina -= parametersManager.ChangeStamina;
            moveComponent.OnMovingEvent -= StartTired;
            moveComponent.OnFinishMovingEvent -= StopTired;
        }

        private void StartTired()
        {
            tirednessComponent.DoTired();
        }

        private void StopTired()
        {
            tirednessComponent.StopTired();
        }

       
    }
}
