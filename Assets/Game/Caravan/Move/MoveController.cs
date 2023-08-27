using Assets.Game;
using Assets.Game.HappeningSystem;
using Entities;
using GameSystems;
using Model.Entities.Happenings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

class MoveController : MonoBehaviour,
    IGameReadyElement, IGameFinishElement, IGameInitElement, IGameStartElement, IGameChangeSceneElement, IGamePauseElement, IGameResumeElement
{
    private IMoveComponent moveComponent;
    private SetupCampManager setupCampManager;
    private IEntity caravanEntity;


    [Inject]
    public void Construct(SetupCampManager setupCampManager, [Inject(Id = "caravan")] IEntity caravan)
    {
        this.setupCampManager = setupCampManager;
        this.caravanEntity = caravan;
    }

    void IGameInitElement.InitGame(IGameSystem _)
    {
        moveComponent = caravanEntity.Element<IMoveComponent>();
    }
    void IGameReadyElement.ReadyGame()
    {
        setupCampManager.OnSetupCamp_Before += moveComponent.Stop;
    }
    void IGameChangeSceneElement.ChangeScene()
    {
        Unsubcribe();
    }
    void IGameStartElement.StartGame()
    {
        moveComponent.Move();
    }
    void IGameFinishElement.FinishGame()
    {
        Unsubcribe();
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
    private void Unsubcribe()
    {
        setupCampManager.OnSetupCamp_Before -= moveComponent.Stop;
    }   
}
