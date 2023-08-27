using Entities;
using GameSystems;
using UnityEngine;
using Zenject;

public class ParamSpendingController : MonoBehaviour, 
    IGameInitElement, IGameReadyElement, IGameChangeSceneElement, IGameFinishElement
{
    private IEntity caravan;
    private IMoveComponent moveComponent;
    private ParamSpendingComponent paramSpendingComponent;

    [Inject]
    public void Construct([Inject(Id = "caravan")] IEntity caravan)
    {
        this.caravan = caravan;
    }

    void IGameInitElement.InitGame(IGameSystem _)
    {
        moveComponent = this.caravan.Element<IMoveComponent>();
        paramSpendingComponent = this.caravan.Element<ParamSpendingComponent>();
    }

    void IGameReadyElement.ReadyGame()
    {
        Subcribe();
    }
    void IGameChangeSceneElement.ChangeScene()
    {
        Unsubscribe();
    }

    void IGameFinishElement.FinishGame()
    {
        Unsubscribe();
    }
    private void Subcribe()
    {
        moveComponent.OnMovingEvent += paramSpendingComponent.SpendParam;
        moveComponent.OnFinishMovingEvent += paramSpendingComponent.StopSpendParam;
    }

    private void Unsubscribe()
    {
        moveComponent.OnMovingEvent -= paramSpendingComponent.SpendParam;
        moveComponent.OnFinishMovingEvent -= paramSpendingComponent.StopSpendParam;
    }
}
