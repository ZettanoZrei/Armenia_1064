using Assets.Game.Parameters;
using Entities;
using GameSystems;
using System;
using UnityEngine;
using Zenject;

namespace Parameters
{
    class TirednessCotroller : MonoBehaviour,
        IGameInitElement, IGameReadyElement, IGameFinishElement, IGameChangeSceneElement
    {
        private IMoveComponent moveComponent;
        private ITirednessComponent tirednessComponent;
        private IEntity caravan;
        private ParametersManager parametersManager;

        [Inject]
        public void Construct([Inject(Id = "caravan")] IEntity caravan, ParametersManager parametersManager)
        {
            this.caravan = caravan;
            this.parametersManager = parametersManager;
        }

        void IGameInitElement.InitGame(IGameSystem _)
        {
            moveComponent = this.caravan.Element<IMoveComponent>();
            tirednessComponent = this.caravan.Element<ITirednessComponent>();
        }


        void IGameReadyElement.ReadyGame()
        {
            tirednessComponent.OnDecreaseStamina += parametersManager.ChangeStamina;
            moveComponent.OnMovingEvent += StartTired;
            moveComponent.OnFinishMovingEvent += StopTired;
        }
        void IGameChangeSceneElement.ChangeScene()
        {
            Unsubscribe();
        }

        void IGameFinishElement.FinishGame()
        {
            Unsubscribe();
        }

        private void Unsubscribe()
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
