using Assets.Game;
using Assets.Game.HappeningSystem;
using Assets.Modules;
using Entities;
using GameSystems.Modules;
using Zenject;

class MoveController : IInitializable,
    ISceneInitialize, 
    ISceneReady, 
    ISceneStart, 
    IScenePause, 
    ISceneResume,
    ISceneFinish 
{
    private IMoveComponent moveComponent;
    private readonly SetupCampManager setupCampManager;
    private readonly IEntity caravanEntity;
    private readonly SignalBus signalBus;

    public MoveController(SetupCampManager setupCampManager, [Inject(Id = "caravan")] IEntity caravan, SignalBus signalBus)
    {
        this.setupCampManager = setupCampManager;
        this.caravanEntity = caravan;
        this.signalBus = signalBus;
    }

    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
    }
    void ISceneInitialize.InitScene()
    {
        moveComponent = caravanEntity.Element<IMoveComponent>();
    }
    void ISceneReady.ReadyScene()
    {
        setupCampManager.OnSetupCamp_Before += moveComponent.Stop;
    }

    void ISceneStart.StartScene()
    {
        moveComponent.Move();
    }
    void ISceneFinish.FinishScene()
    {
        setupCampManager.OnSetupCamp_Before -= moveComponent.Stop;
        moveComponent.Stop();
    }
    void IScenePause.PauseScene()
    {
        moveComponent.Stop();
    }

    void ISceneResume.ResumeScene()
    {
        moveComponent.Move();
    }

}
