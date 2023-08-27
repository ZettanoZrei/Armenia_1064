using Assets.Game.Configurations;
using Assets.Game.Parameters;
using Entities;
using Model.Entities.Answers;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Zenject;


namespace Assets.Game.Camp
{
    public class RestModel
    {
        private readonly ParametersManager parametersManager;
        private readonly RestConfig restConfiguration;

        private readonly Dictionary<ParameterType, string> finalMessages = new Dictionary<ParameterType, string>
        {
            {ParameterType.Food, "Люди до поздней ночи обыскивали округу и смогли немного пополнить свои припасы. " +
                "Однако они и не успели помолиться на ночь" },
            {ParameterType.Stamina, "Люди смоги хорошо выпасть и отдохнуть. Однако они не успели пополнить запасы" },
            {ParameterType.Spirit, "Люди молились почти всю ночь, что укрепило их моральных дух. " +
                "Однако они не успели пополнить запасы" }
        };
        public RestModel(ParametersManager parametersManager, RestConfig restConfiguration)
        {
            this.parametersManager = parametersManager;
            this.restConfiguration = restConfiguration;
        }


        public Consequences Rest(ParameterType type)
        {
            var consequences = new Consequences();
            switch (type)
            {
                case ParameterType.Food:
                    PlusFood_MinusSpirit(consequences);
                    break;
                case ParameterType.Spirit:
                    PlusSpirit_MinusStamina(consequences);
                    break;
                case ParameterType.Stamina:
                    PlusStamina_MinusFood(consequences);
                    break;
            }
            consequences.Message = finalMessages[type];
            consequences.IsMessage = true;
            return consequences;
        }

        private void PlusFood_MinusSpirit(Consequences consequences)
        {
            parametersManager.ChangeFood(restConfiguration.plusParam);
            parametersManager.ChangeSpirit(restConfiguration.minusParam);
            consequences.ParamConsequences.Add(new SingleParamConsequences
            {
                ParameterType = ParameterType.Food,
                Value = restConfiguration.plusParam,
            });
            consequences.ParamConsequences.Add(new SingleParamConsequences
            {
                ParameterType = ParameterType.Spirit,
                Value = restConfiguration.minusParam,
            });
        }
        private void PlusSpirit_MinusStamina(Consequences consequences)
        {
            parametersManager.ChangeSpirit(restConfiguration.plusParam);
            parametersManager.ChangeStamina(restConfiguration.minusParam);
            consequences.ParamConsequences.Add(new SingleParamConsequences
            {
                ParameterType = ParameterType.Spirit,
                Value = restConfiguration.plusParam,
            });
            consequences.ParamConsequences.Add(new SingleParamConsequences
            {
                ParameterType = ParameterType.Stamina,
                Value = restConfiguration.minusParam,
            });
        }
        private void PlusStamina_MinusFood(Consequences consequences)
        {
            parametersManager.ChangeStamina(restConfiguration.plusParam);
            parametersManager.ChangeFood(restConfiguration.minusParam);
            consequences.ParamConsequences.Add(new SingleParamConsequences
            {
                ParameterType = ParameterType.Stamina,
                Value = restConfiguration.plusParam,
            });
            consequences.ParamConsequences.Add(new SingleParamConsequences
            {
                ParameterType = ParameterType.Food,
                Value = restConfiguration.minusParam,
            });
        }
    }
}
