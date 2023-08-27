using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Parameters.EndedParamSystem
{
    internal class DEBUGEndedController : MonoBehaviour
    {
        public GameObject prefab;
        public Transform popupContainer;
        [Inject] EndedParamMechanics endedParamManager;
        public ParameterType parameterType;

        [Inject] ParametersManager parametersManager;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Instantiate(prefab, popupContainer);
                StartTimer();
            }
        }

        [ContextMenu("StartTimer")]
        public void StartTimer()
        {
            endedParamManager.BeginTimer(parameterType);
        }


        [ContextMenu("EndTimer")]
        public void EndTimer()
        {
            endedParamManager.FinishTimer(parameterType);
        }

        [ContextMenu("DeleteResource")]
        public void DeleteResource()
        {
            parametersManager.ChangeSpirit(-20);
        }
    }
}
