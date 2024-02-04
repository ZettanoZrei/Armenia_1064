using Assets.Game.Configurations;
using Assets.Game.Parameters;
using Assets.Modules;
using Assets.Modules.UI;
using GameSystems.Modules;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;


namespace Assets.Game.Camp
{
    class RestController : IInitializable,
        ISceneReady,
        ISceneFinish,
        ISceneStart
    {
        private readonly SimpleButton restButton;
        private readonly RestManager restManager;
        private readonly RestConfig restConfiguration;
        private readonly ParametersManager parametersManager;
        private readonly RestLocker restLocker;
        private readonly SignalBus signalBus;

        public RestController(RestManager restManager, RestConfig restConfiguration, ParametersManager parametersManager,
            RestLocker restLocker, SignalBus signalBus, [Inject(Id = "restButton")] SimpleButton restButton)
        {
            this.restManager = restManager;
            this.restConfiguration = restConfiguration;
            this.parametersManager = parametersManager;
            this.restLocker = restLocker;
            this.signalBus = signalBus;
            this.restButton = restButton;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        void ISceneReady.ReadyScene()
        {
            restButton.OnClick += DoRest;
            restManager.OnRest += BlockRest;
        }

      
        void ISceneStart.StartScene()
        {
            if(!CheckRestPosibility(restConfiguration, parametersManager))
                restButton.SetActiveButton(false);
        }
        void ISceneFinish.FinishScene()
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
