using Assets.Game.Configurations;
using Entities;
using Zenject;
using System.Timers;
using UnityEngine;
using GameSystems.Modules;
using Assets.Modules;

namespace Assets.Game.Timer
{
    class TimeTravelController : IInitializable, 
        ILateTickable,
        ISceneInitialize,
        ISceneReady, 
        ISceneFinish
    {
        private readonly TimeMechanics timeMechanics;
        private readonly TimeConfig config;
        private readonly IEntity caravan;
        private readonly SignalBus signalBus;
        private IMoveComponent moveComponent;

        private float time;
        private TimerState timerState;

        public TimeTravelController(TimeMechanics timeManager, TimeConfig consfig, [Inject(Id = "caravan")] IEntity caravan,
            SignalBus signalBus)
        {
            this.timeMechanics = timeManager;
            this.config = consfig;
            this.caravan = caravan;
            this.signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        void ILateTickable.LateTick()
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
        void ISceneInitialize.InitScene()
        {
            this.moveComponent = caravan.Element<IMoveComponent>();
        }
        void ISceneReady.ReadyScene()
        {
            moveComponent.OnMovingEvent += TimerBegin;
            moveComponent.OnFinishMovingEvent += TimerStop;
        }

        void ISceneFinish.FinishScene()
        {
            moveComponent.OnMovingEvent -= TimerBegin;
            moveComponent.OnFinishMovingEvent -= TimerStop;
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
