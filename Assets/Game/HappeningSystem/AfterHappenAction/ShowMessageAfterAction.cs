using Assets.Game.Message;
using Assets.Modules;
using Entities;
using Model.Entities.Answers;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class ShowMessageAfterAction : IAfterHappenAction, ICallBack
    {
        private readonly MessageManager manager;
        private readonly Consequences consequences;
        private ICallBack afterActionManagerCallBack;
        private const float delayTime = 0.2f;
        public ShowMessageAfterAction(MessageManager manager, Consequences consequences)
        {
            this.manager = manager;
            this.consequences = consequences;
        }

        void IAfterHappenAction.Do(ICallBack callBack)
        {
            afterActionManagerCallBack = callBack;
            //WorldState.Instance.StartCoroutine(DelayLaunch());            
        }     
        IAfterHappenAction ICloneable<IAfterHappenAction>.Clone()
        {
            throw new NotImplementedException();
        }
        void ICallBack.Return(object obj)
        {
            afterActionManagerCallBack.Return(null);
        }

        //IEnumerator DelayLaunch()
        //{

        //    //yield return new WaitForSeconds(delayTime);
        //    //manager.Perform(consequences, this);
        //}
    }
}
