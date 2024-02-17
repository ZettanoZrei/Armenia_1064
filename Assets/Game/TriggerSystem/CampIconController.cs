using Assets.Modules;
using GameSystems.Modules;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class CampIconController : IInitializable, ISceneReady, ISceneFinish
    {
        private readonly SignalBus signalBus;
        private readonly HappeningManager happeningManager;
        private IEnumerable<CampIcon> campIcons;

        public CampIconController(SignalBus signalBus, HappeningManager happeningManager)
        {
            this.signalBus = signalBus;
            this.happeningManager = happeningManager;
        }
       
        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }

        void ISceneReady.ReadyScene()
        {
            //it has to be here because of IconController
            campIcons = MonoBehaviour.FindObjectsOfType<CampIcon>(); //TODO перенести в отдельный контролер

            foreach (var trigger in campIcons)
                trigger.OnIconClick += LaunchHappenOutOfQueue;
        }
        void ISceneFinish.FinishScene()
        {
            foreach (var trigger in campIcons)
                trigger.OnIconClick -= LaunchHappenOutOfQueue;
        }
        //for camp quest
        private void LaunchHappenOutOfQueue(string quest)
        {
            happeningManager.LaunchHappenWithoutQueue(quest);
        }
    }
}
