using Assets.Game.Camp;
using Assets.Game.Configurations;
using Assets.Modules;
using GameSystems.Modules;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Timer
{
    class TimeRestController : IInitializable, 
        IGameReadyElement, 
        IGameFinishElement
    {
        private readonly TimeMechanics timeManager;
        private readonly RestManager restManager;
        private readonly TimeConfig config;
        private readonly SignalBus signalBus;

        public TimeRestController(TimeMechanics parametersManager, RestManager restManager, TimeConfig config, SignalBus signalBus)
        {
            this.timeManager = parametersManager;
            this.restManager = restManager;
            this.config = config;
            this.signalBus = signalBus;
        }
        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        void IGameReadyElement.ReadyGame()
        {
            restManager.OnRest += AddTime;
        }

        void IGameFinishElement.FinishGame()
        {
            restManager.OnRest -= AddTime;
        }

        private Task AddTime()
        {
            timeManager.AddDay(config.restDay);
            return Task.CompletedTask;
        }       
    }
}
