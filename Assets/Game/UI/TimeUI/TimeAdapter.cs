using Assets.Game.Timer;
using Assets.Modules;
using GameSystems.Modules;
using UnityEngine;
using Zenject;

namespace Assets.Game.UI.TimeUI
{
    class TimeAdapter : IInitializable, 
        IGameReadyElement, 
        IGameFinishElement, 
        IGameStartElement
    {
        [SerializeField] private TimeView view;
        private readonly TimeMechanics model;
        private readonly SignalBus signalBus;

        public TimeAdapter(SignalBus signalBus, TimeMechanics timeMechanics, TimeView view)
        {
            this.signalBus = signalBus;
            this.model = timeMechanics;
            this.view = view;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        void IGameReadyElement.ReadyGame()
        {
            model.OnTimeChanged += view.SetTime;
            model.OnDayChanged += view.SetDay;
        }       

        void IGameStartElement.StartGame()
        {
            view.SetDay(model.Days);
            view.SetTime(model.Time);
        }

        void IGameFinishElement.FinishGame()
        {
            model.OnTimeChanged -= view.SetTime;
            model.OnDayChanged -= view.SetDay;
        }       
    }
}
