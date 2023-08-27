using Assets.Game.Configurations;
using Entities;
using Zenject;
using System.Timers;
using UnityEngine;
using GameSystems;

namespace Assets.Game.Timer
{
    class TimeTravelController : MonoBehaviour, 
        IGameReadyElement, IGameFinishElement, IGameChangeSceneElement
    {
        private TimeMechanics timeMechanics;
        private TimeConfig config;
        private IMoveComponent moveComponent;

        private float time;
        private TimerState timerState;

        [Inject]
        public void Construct(TimeMechanics timeManager, TimeConfig consfig, [Inject(Id = "caravan")] IEntity caravan)
        {
            this.timeMechanics = timeManager;
            this.config = consfig;
            this.moveComponent = caravan.Element<IMoveComponent>();
        }

        void IGameReadyElement.ReadyGame()
        {
            moveComponent.OnMovingEvent += TimerBegin;
            moveComponent.OnFinishMovingEvent += TimerStop;
        }

        void IGameFinishElement.FinishGame()
        {
            moveComponent.OnMovingEvent -= TimerBegin;
            moveComponent.OnFinishMovingEvent -= TimerStop;
        }

        void IGameChangeSceneElement.ChangeScene()
        {
            moveComponent.OnMovingEvent -= TimerBegin;
            moveComponent.OnFinishMovingEvent -= TimerStop;
        }

        private void LateUpdate()
        {
            if (timerState == TimerState.Start)
            {
                time += Time.deltaTime;
                if (time > 1)
                {
                    AddTravelTime();
                }
            }
        }

        private void AddTravelTime()
        {
            float dayValue = time / config.secPerDay;
            timeMechanics.AddDay(dayValue);
            time = 0;
        }

        private void TimerStop()
        {
            timerState = TimerState.Stop;
        }

        private void TimerBegin()
        {
            timerState = TimerState.Start;
        }

        
        enum TimerState
        {
            Start,
            Stop,
        }
    }
}
