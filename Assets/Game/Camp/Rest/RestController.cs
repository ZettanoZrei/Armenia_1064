using Assets.Game.Configurations;
using Assets.Game.Parameters;
using Assets.Modules.UI;
using GameSystems;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;


namespace Assets.Game.Camp
{
    class RestController : MonoBehaviour, 
        IGameReadyElement, IGameFinishElement, IGameStartElement
    {
        [SerializeField] private SimpleButton restButton;

        private RestManager restManager;
        private RestConfig restConfiguration;
        private ParametersManager parametersManager;
        private RestLocker restLocker;

        [Inject]
        public void Construct(RestManager restManager, RestConfig restConfiguration, ParametersManager parametersManager,
            RestLocker restLocker)
        {
            this.restManager = restManager;
            this.restConfiguration = restConfiguration;
            this.parametersManager = parametersManager;
            this.restLocker = restLocker;
        }

        void IGameReadyElement.ReadyGame()
        {
            restButton.OnClick += DoRest;
            restManager.OnRest += BlockRest;
        }

      
        void IGameStartElement.StartGame()
        {
            if(!CheckRestPosibility(restConfiguration, parametersManager))
                restButton.SetActiveButton(false);
        }
        void IGameFinishElement.FinishGame()
        {
            restButton.OnClick -= DoRest;
            restManager.OnRest -= BlockRest;
        }
        private void DoRest()
        {
            restManager.RestBegin();
        }

        private Task BlockRest()
        {
            restLocker.IsRestOnceBlock = true;
            restButton.SetActiveButton(false);
            return Task.CompletedTask;
        }

        private bool CheckRestPosibility(RestConfig restConfiguration, ParametersManager parametersManager)
        {
            var minusParam = Math.Abs(restConfiguration.minusParam);
            var paramBlock = parametersManager.Food.Value < minusParam && parametersManager.Stamina.Value < minusParam
                && parametersManager.Spirit.Value < minusParam;

            return !paramBlock && !restLocker.IsRestOnceBlock;
        }       
    }
}
