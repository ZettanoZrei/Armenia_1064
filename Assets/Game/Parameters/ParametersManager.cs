using Model.Entities.Answers;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Game.Parameters
{
    public class ParametersManager
    {
        public ReactiveProperty<int> People { get; private set; } = new ReactiveProperty<int>();
        public ReactiveProperty<float> Stamina { get; private set; } = new ReactiveProperty<float>();
        public ReactiveProperty<float> Spirit { get; private set; } = new ReactiveProperty<float>();
        public ReactiveProperty<float> Food { get; private set; } = new ReactiveProperty<float>();


        public void ChangePeople(int value)
        {
            People.Value = Math.Max(People.Value + value, 0);
        }
        public void ChangeFood(float value)
        {
            Food.Value = Mathf.Clamp(Food.Value + value, 0, 100);
        }
        public void ChangeSpirit(float value)
        {
            Spirit.Value = Mathf.Clamp(Spirit.Value + value, 0, 100);
        }

        public void ChangeStamina(float value)
        {
            Stamina.Value = Mathf.Clamp(Stamina.Value + value, 0, 100);
        }


        public void ChangeParameter(SingleParamConsequences consequences)
        {
            switch (consequences.ParameterType)
            {
                case ParameterType.Food:
                    ChangeFood(consequences.Value);
                    break;
                case ParameterType.Spirit:
                    ChangeSpirit(consequences.Value);
                    break;
                case ParameterType.Stamina:
                    ChangeStamina(consequences.Value);
                    break;
                case ParameterType.People:
                    ChangePeople(consequences.Value);
                    break;
            }
        }


        //for start and continue
        public void SetParameter(float value, ParameterType name)
        {
            switch(name)
            {
                case ParameterType.Stamina:
                    Stamina.Value = value;
                    break;
                case ParameterType.Spirit:
                    Spirit.Value = value;
                    break;
                case ParameterType.Food:
                    Food.Value = value;
                    break;
                case ParameterType.People:
                    People.Value = (int)value;
                    break;
            }
        }

        public void Clear()
        {
            People.Value = 0;
            Food.Value = 0;
            Spirit.Value = 0;
            Stamina.Value = 0;
        }
    }
}
