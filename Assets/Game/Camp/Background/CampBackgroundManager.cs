using Assets.Modules;
using Entities;
using GameSystems.Modules;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Game.Camp.Background
{
    public class CampBackgroundManager : IInitializable, IGameReadyElement
    {
        private readonly Transform backContainer;
        private readonly SignalBus signalBus;
        private readonly CampIncomingData сampIncomingData;

        public CampBackgroundManager(CampIncomingData сampIncomingData, [Inject(Id = "backContainer")] Transform backContainer,
            SignalBus signalBus)
        {
            this.сampIncomingData = сampIncomingData;
            this.backContainer = backContainer;
            this.signalBus = signalBus;
        }

        public CampBackground CampBackground { get; private set; }
        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }

        void IGameReadyElement.ReadyGame()
        {
            var path = $"Background/Camp/{сampIncomingData.CampImagePrefab}";
            this.CampBackground = MonoBehaviour.Instantiate(Resources.Load<MonoEntity>(path), backContainer).Element<CampBackground>();
        }
    }
}
