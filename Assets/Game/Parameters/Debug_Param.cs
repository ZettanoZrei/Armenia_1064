using Model.Entities.Answers;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Parameters
{
    internal class Debug_Param : MonoBehaviour
    {
        [Inject] private ParametersManager parametersManager;

        public ParameterType parameterType;
        public int value;

        [ContextMenu("ChangeParam")]
        public void ChangeParam()
        {
            parametersManager.ChangeParameter(new SingleParamConsequences { ParameterType= parameterType, Value = value });
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                ChangeParam();
            }
        }
    }
}
