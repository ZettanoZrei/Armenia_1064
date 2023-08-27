using Assets.Game.Camp;
using Assets.Game.Configurations;
using GameSystems;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Timer
{
    class TimeRestController : MonoBehaviour, 
        IGameReadyElement, IGameFinishElement, IGameChangeSceneElement
    {
        private TimeMechanics timeManager;
        private RestManager restManager;
        private TimeConfig config;


        [Inject]
        public void Construct(TimeMechanics parametersManager, RestManager restManager, TimeConfig config)
        {
            this.timeManager = parametersManager;
            this.restManager = restManager;
            this.config = config;
        }

        void IGameReadyElement.ReadyGame()
        {
            restManager.OnRest += AddTime;
        }

        void IGameFinishElement.FinishGame()
        {
            restManager.OnRest -= AddTime;
        }

        void IGameChangeSceneElement.ChangeScene()
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
