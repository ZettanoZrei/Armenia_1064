using Assets.Game;
using Assets.Modules;
using GameSystems.Modules;
using UnityEngine;
using Zenject;

public class PopupPauseController : IInitializable, 
    IGameReadyElement, 
    IGameFinishElement
{
    private readonly PopupManager popupManager;
    private readonly SignalBus signalBus;
    private readonly SceneManager sceneManager;

    [Inject]
    public PopupPauseController(PopupManager popupManager, SignalBus signalBus, SceneManager sceneManager)
    {
        this.popupManager = popupManager;
        this.signalBus = signalBus;
        this.sceneManager = sceneManager;
    }

    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
    }
    void IGameReadyElement.ReadyGame()
    {
        popupManager.OnPopupChanged += PauseHandle;
    }   

    void IGameFinishElement.FinishGame()
    {
        popupManager.OnPopupChanged -= PauseHandle;
    }
    private void PauseHandle(object sender, PopupManager.PopupChangedEventArgs e)
    {
        if(e.Pause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    private void Pause()
    {
        Time.timeScale = 0;
        sceneManager.PauseGame();
    }

    private void Resume()
    {
        sceneManager.ContinueGame();
        Time.timeScale = 1;
    }
}
