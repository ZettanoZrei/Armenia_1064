using Assets.Modules;
using Entities;
using GameSystems.Modules;
using UnityEngine;
using Zenject;

public class ParamSpendingController : IInitializable, 
    ISceneInitialize, 
    ISceneReady, 
    ISceneFinish
{
    private readonly IEntity caravan;
    private readonly SignalBus signalBus;
    private IMoveComponent moveComponent;
    private ParamSpendingComponent paramSpendingComponent;

    [Inject]
    public ParamSpendingController([Inject(Id = "caravan")] IEntity caravan, SignalBus signalBus)
    {
        this.caravan = caravan;
        this.signalBus = signalBus;
    }
    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
    }
    void ISceneInitialize.InitScene()
    {
        moveComponent = this.caravan.Element<IMoveComponent>();
        paramSpendingComponent = this.caravan.Element<ParamSpendingComponent>();
    }

    void ISceneReady.ReadyScene()
    {
        moveComponent.OnMovingEvent += paramSpendingComponent.SpendParam;
        moveComponent.OnFinishMovingEvent += paramSpendingComponent.StopSpendParam;
    }

    void ISceneFinish.FinishScene()
    {
        moveComponent.OnMovingEvent -= paramSpendingComponent.SpendParam;
        moveComponent.OnFinishMovingEvent -= paramSpendingComponent.StopSpendParam;
    }   
}
