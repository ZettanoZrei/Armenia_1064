using Assets.Modules;
using Entities;
using GameSystems;
using JetBrains.Annotations;
using Model.Entities.Happenings;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class LaunchHappeningAfterAction : IAfterHappenAction
    {
        private readonly HappeningManager happeningManager;
        private readonly string questTitle;
        private readonly bool delay;
        private const float delayTime = 1f;
        private ICallBack afterActionManagerCallBack;
        public LaunchHappeningAfterAction(HappeningManager happeningManager, string questTitle, bool delay) 
        {
            this.happeningManager = happeningManager;
            this.questTitle = questTitle;
            this.delay = delay;
        }

        void IAfterHappenAction.Do(ICallBack callBack)
        {
            this.afterActionManagerCallBack = callBack;           
            WorldState.Instance.StartCoroutine(DelayLaunch());
        }

        IAfterHappenAction ICloneable<IAfterHappenAction>.Clone()
        {
            return (IAfterHappenAction)MemberwiseClone();
        }
       
        IEnumerator DelayLaunch()
        {
            var delay = this.delay ? delayTime : 0;
            yield return new WaitForSeconds(delay);
            this.happeningManager.LaunchHappenWithoutQueue(questTitle);
            this.happeningManager.OnFinishHappening += CallBack;
        }

        private void CallBack(Happening _)
        {
            this.happeningManager.OnFinishHappening -= CallBack;
            this.afterActionManagerCallBack.Return(null);
        }
    }
}
