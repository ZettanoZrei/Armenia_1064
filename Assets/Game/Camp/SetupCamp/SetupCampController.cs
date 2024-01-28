using Entities;
using Interfeces;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Assets.Game.Stoppage;
using Assets.Game;
using Zenject;
using Assets.Modules.UI;
using Assets.Game.Camp;
using System.ComponentModel;
using GameSystems.Modules;
using Assets.Modules;

class SetupCampController : IInitializable,
    IGameReadyElement, 
    IGameFinishElement, 
    IGameInitElement
{
    private ITriggerComponent caravanTrigger;
    private readonly SetupCampManager setupCampManager;
    private readonly IEntity caravan;
    private readonly SignalBus signalBus;
    private readonly CampIncomingData campIncomingData;
    private readonly SimpleButton setupCampButton;

    public SetupCampController(SetupCampManager setupCampManager, CampIncomingData campIncomingData, [Inject(Id = "caravan")] IEntity caravan,
        SignalBus signalBus, [Inject(Id = "setupCampButton")] SimpleButton setupCampButton)
    {
        this.setupCampManager = setupCampManager;
        this.caravan = caravan;
        this.signalBus = signalBus;
        this.campIncomingData = campIncomingData;
        this.setupCampButton = setupCampButton;
    }

    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
    }
    void IGameInitElement.InitGame()
    {
        caravanTrigger = caravan.Element<ITriggerComponent>();
    }
    void IGameReadyElement.ReadyGame()
    {
        setupCampButton.OnClick += SetupCamp;
        caravanTrigger.OnTriggerEnter += StoppageCollision;
    }
    void IGameFinishElement.FinishGame()
    {
        setupCampButton.OnClick -= SetupCamp;
        caravanTrigger.OnTriggerEnter -= StoppageCollision;
    }


    private void StoppageCollision(Collider2D collider)
    {
        if (collider.TryGetComponent(out StoppageTrigger stoppage))
        {
            if (stoppage.IsDone)
                return;
            stoppage.IsDone = true;

            if (!string.IsNullOrEmpty(stoppage.CampBackground))
                campIncomingData.CampImagePrefab = stoppage.CampBackground;

            if (stoppage.IsForced)
                setupCampManager.SetupCamp(stoppage.DialogAvailable);
        }
    }

    private void SetupCamp()
    {
        setupCampManager.SetupCamp();
    }

    
}
