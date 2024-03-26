using Assets.DialogSystem.Scripts;
using Assets.Modules;
using GameSystems.Modules;
using Packages.Rider.Editor.UnitTesting;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class CampIconController : IInitializable, ISceneReady, ISceneFinish
    {
        private readonly SignalBus signalBus;
        private readonly DialogAgent dialogAgent;
        private IEnumerable<CampIcon> campIcons;

        public CampIconController(SignalBus signalBus, DialogAgent dialogAgent)
        {
            this.signalBus = signalBus;
            this.dialogAgent = dialogAgent;
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
                trigger.OnIconClick += LaunchDialog;
        }
        void ISceneFinish.FinishScene()
        {
            foreach (var trigger in campIcons)
                trigger.OnIconClick -= LaunchDialog;
        }
        //for camp quest
        private async void LaunchDialog(string quest)
        {
            await dialogAgent.LaunchDialog(quest, "Conversation");
        }
    }
}
