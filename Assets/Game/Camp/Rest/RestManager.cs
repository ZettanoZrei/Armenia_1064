using Assets.Game.HappeningSystem;
using Assets.Game.Message;
using Assets.Modules;
using Entities;
using Model.Entities.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;


namespace Assets.Game.Camp
{
    class RestManager : ICallBack
    {
        private readonly IEnumerator<IRestStep> restStepsQeueuEnumerator;
        public event Func<Task> OnRest;
        public RestManager(List<IRestStep> restStepsQueue)
        {
            this.restStepsQeueuEnumerator = restStepsQueue.GetEnumerator();
        }

        public void RestBegin()
        {
            restStepsQeueuEnumerator.Reset();   
            ((ICallBack)this).Return(true);
        }
        void ICallBack.Return(object result)
        {
            DoNextStep((bool)result);
        }

        private void DoNextStep(bool result)
        {
            if(!restStepsQeueuEnumerator.MoveNext())
            {
                OnRest?.Invoke();
                return;
            }
            if(result)
            {
                restStepsQeueuEnumerator.Current.Execute(this);
            }
        }
    }
}
