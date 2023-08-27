using Entities;
using GameSystems;
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

class SetupCampController : MonoBehaviour,
    IGameReadyElement, IGameFinishElement, IGameInitElement
{
    private ITriggerComponent caravanTrigger;
    private SetupCampManager setupCampManager;
    private IEntity caravan;
    private CampIncomingData campIncomingData;
    [SerializeField] private SimpleButton setupCampButton;

    [Inject]
    public void Construct(SetupCampManager setupCampManager, CampIncomingData campIncomingData, [Inject(Id = "caravan")] IEntity caravan)
    {
        this.setupCampManager = setupCampManager;
        this.caravan = caravan;
        this.campIncomingData = campIncomingData;
    }
    void IGameInitElement.InitGame(IGameSystem _)
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
