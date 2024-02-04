using Assets.Game;
using Assets.Game.HappeningSystem;
using Assets.Modules;
using Entities;
using GameSystems.Modules;
using Zenject;

class MoveController : 
    IGameReadyElement, 
    IGameFinishElement, 
    IGameInitElement, 
    IGameStartElement, 
    IGamePauseElement, 
    IGameResumeElement
{
    private IMoveComponent moveComponent;
    private readonly SetupCampManager setupCampManager;
    private readonly SignalBus signalBus;
    private readonly IEntity caravanEntity;


    [Inject]
    public MoveController(SetupCampManager setupCampManager, SignalBus signalBus, [Inject(Id = "caravan")] IEntity caravan)
    {
        this.setupCampManager = setupCampManager;
        this.signalBus = signalBus;
        this.caravanEntity = caravan;
    }
    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
    }
    void IGameInitElement.InitGame()
    {
        moveComponent = caravanEntity.Element<IMoveComponent>();
    }
    void IGameReadyElement.ReadyGame()
    {
        setupCampManager.OnSetupCamp_Before += moveComponent.Stop;
    }

    void IGameStartElement.StartGame()
    {
        moveComponent.Move();
    }
    void IGameFinishElement.FinishGame()
    {
        setupCampManager.OnSetupCamp_Before -= moveComponent.Stop;
        moveComponent.Stop();
    }
    void IGamePauseElement.PauseGame()
    {
        moveComponent.Stop();
    }

    void IGameResumeElement.ResumeGame()
    {
        moveComponent.Move();
    }   
}
