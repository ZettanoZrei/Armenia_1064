using Assets.Modules;
using Entities;
using GameSystems.Modules;
using UnityEngine;
using Zenject;

public class ParamSpendingController : IInitializable, 
    IGameInitElement, 
    IGameReadyElement, 
    IGameFinishElement
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
    void IGameInitElement.InitGame()
    {
        moveComponent = this.caravan.Element<IMoveComponent>();
        paramSpendingComponent = this.caravan.Element<ParamSpendingComponent>();
    }

    void IGameReadyElement.ReadyGame()
    {
        moveComponent.OnMovingEvent += paramSpendingComponent.SpendParam;
        moveComponent.OnFinishMovingEvent += paramSpendingComponent.StopSpendParam;
    }

    void IGameFinishElement.FinishGame()
    {
        moveComponent.OnMovingEvent -= paramSpendingComponent.SpendParam;
        moveComponent.OnFinishMovingEvent -= paramSpendingComponent.StopSpendParam;
    }   
}
