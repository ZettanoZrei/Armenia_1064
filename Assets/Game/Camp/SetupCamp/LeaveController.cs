using Assets.Modules;
using Assets.Modules.UI;
using Entities;
using GameSystems.Modules;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

class LeaveController : IInitializable,
    IGameReadyElement, 
    IGameFinishElement
{
    private SetupCampManager setupCampManager;
    private readonly SignalBus signalBus;
    private SimpleButton leaveButton;

    public LeaveController(SetupCampManager setupCampManager, SignalBus signalBus, [Inject(Id = "leaveButton")] SimpleButton leaveButton)
    {
        this.setupCampManager = setupCampManager;
        this.signalBus = signalBus;
        this.leaveButton = leaveButton;
    }
    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
    }
    void IGameReadyElement.ReadyGame()
    {
        leaveButton.OnClick += LeaveCamp;
    }
    void IGameFinishElement.FinishGame()
    {
        leaveButton.OnClick -= LeaveCamp;
    }
    private void LeaveCamp()
    {
        setupCampManager.LeaveCamp();
    }

    
}


