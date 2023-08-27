using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Camp
{
    class OptionSignComponent : MonoBehaviour
    {
        [SerializeField] private List<ParamSign> paramSignList = new List<ParamSign>();


        public void SetParamForSigns(NightWorkOptionInfo info)
        {
            var plusParam = paramSignList.First(x => x.type == info.ParameterTypePlus);
            plusParam.view.SetParam(info.PlusValue);
            plusParam.view.SetActive(true);

            var minusParam = paramSignList.First(x => x.type == info.ParameterTypeMinus);
            minusParam.view.SetParam(info.MinusValue);
            minusParam.view.SetActive(true);
        }


    }

    [Serializable]
    public class ParamSign
    {
        public ParameterType type;
        public MessageParamView view;
    }
}
