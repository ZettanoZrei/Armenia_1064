using Assets.Game.Configurations;
using Assets.Game.Parameters;
using Entities;
using Model.Entities.Answers;
using Model.Types;
using System;
using System.Collections.Generic;
using Zenject;


namespace Assets.Game.Camp
{
    public class NightWorkModel
    {
        private readonly ParametersManager parametersManager;
        private readonly RestConfig restConfiguration;

        public event Action<ParameterType> OnChosenNighWorkParam;

        public NightWorkModel(ParametersManager parametersManager, RestConfig restConfiguration)
        {
            this.parametersManager = parametersManager;
            this.restConfiguration = restConfiguration;
        }
        private bool CheckIfParamEnough(ParameterType type)
        {
            switch (type)
            {
                case ParameterType.Food:
                    return parametersManager.Spirit.Value > Math.Abs(restConfiguration.minusParam);
                case ParameterType.Stamina:
                    return parametersManager.Food.Value > Math.Abs(restConfiguration.minusParam);
                case ParameterType.Spirit:
                    return parametersManager.Stamina.Value > Math.Abs(restConfiguration.minusParam);
            }
            throw new Exception("Unknown type!");
        }
        public List<NightWorkOptionInfo> GetNightWorkOptions()
        {
            var spiritWork = new NightWorkOptionInfo
            {
                Answer = new Answer
                {
                    Text = "Помолиться",
                    Index = (int)ParameterType.Spirit,
                    IsRestriction = !CheckIfParamEnough(ParameterType.Spirit)
                },
                ParameterTypePlus = ParameterType.Spirit,
                ParameterTypeMinus = ParameterType.Stamina,
                PlusValue = restConfiguration.plusParam,
                MinusValue = restConfiguration.minusParam
            };
            var foodWork = new NightWorkOptionInfo
            {
                Answer = new Answer
                { 
                    Text="Поискать в округе что-нибудь съестное", 
                    Index = (int)ParameterType.Food,
                    IsRestriction = !CheckIfParamEnough(ParameterType.Food) 
                },
                ParameterTypePlus = ParameterType.Food,
                ParameterTypeMinus = ParameterType.Spirit,
                PlusValue = restConfiguration.plusParam,
                MinusValue = restConfiguration.minusParam
            };
            var staminaWork = new NightWorkOptionInfo
            {
                Answer = new Answer
                {
                    Text = "Отдохнуть и постaраться набраться сил",
                    Index = (int)ParameterType.Stamina,
                    IsRestriction = !CheckIfParamEnough(ParameterType.Stamina)
                },
                ParameterTypePlus = ParameterType.Stamina,
                ParameterTypeMinus = ParameterType.Food,
                PlusValue = restConfiguration.plusParam,
                MinusValue = restConfiguration.minusParam
            };

            return new List<NightWorkOptionInfo> { spiritWork, foodWork, staminaWork }; 
        }

        public void NightWorkChosen(int? type)
        {
            OnChosenNighWorkParam?.Invoke((ParameterType)type);
        }
    }
}
