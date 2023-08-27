using Assets.Game.Camp;
using Assets.Game.Message;
using Assets.Modules;
using Entities;
using Model.Entities.Answers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem.AfterHappenAction
{
    class AfterActionManager : ICallBack
    {
        private readonly List<IAfterHappenAction> afterHappenActions = new List<IAfterHappenAction>();
        private IEnumerator<IAfterHappenAction> enumerator;
        private readonly HappeningManager happeningManager;

        private readonly SetupCampManager setupCampManager; 
        private readonly MySceneManager sceneManager;


        public AfterActionManager(HappeningManager happeningManager, SetupCampManager setupCampManager, MySceneManager sceneManager)
        {
            this.happeningManager = happeningManager;
            this.setupCampManager = setupCampManager;
            this.sceneManager = sceneManager;
        }
        public void SetAction(AfterAction afterAction)
        {
            afterHappenActions.Clear();
            

            if (afterAction.SetupCamp)
                afterHappenActions.Add(new SetupCampAfterAction(setupCampManager, sceneManager, afterAction.DialogAvailable));
            else if (afterAction.LeaveCamp)
                afterHappenActions.Add(new LeaveCampAfterAction(setupCampManager, sceneManager));


            if (afterAction.IsActivateQuest && !string.IsNullOrEmpty(afterAction.Quest))
            {
                if (afterAction.Immediately)
                    afterHappenActions.Add(new LaunchHappeningAfterAction(happeningManager, afterAction.Quest, afterAction.SetupCamp));
                else
                    afterHappenActions.Add(new ActivateHappeningAfterAction(happeningManager, afterAction.Quest));
            }

            this.enumerator = afterHappenActions.GetEnumerator();
        }

        public void Do()
        {
            (this as ICallBack).Return(null);
        }

        void ICallBack.Return(object _)
        {
            if(enumerator == null) return;
            if (enumerator.MoveNext())
            {
                enumerator.Current.Do(this);
            }
        }
    }
}
