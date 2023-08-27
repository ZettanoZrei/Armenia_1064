using Assets.Modules.UI;
using Entities;
using GameSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

class LeaveController : MonoBehaviour,
    IGameReadyElement, IGameFinishElement
{
    private SetupCampManager setupCampManager;
    [SerializeField] private SimpleButton leaveButton;

    [Inject]
    public void Construct(SetupCampManager setupCampManager)
    {
        this.setupCampManager = setupCampManager;
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


