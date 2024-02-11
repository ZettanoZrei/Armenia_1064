using Assets.Game;
using Assets.Modules;
using GameSystems.Modules;
using UnityEngine;
using Zenject;

public class PopupPauseController : IInitializable, 
    ISceneReady, 
    ISceneFinish
{
    private readonly PopupManager popupManager;
    private readonly SignalBus signalBus;
    private readonly SceneScriptManager sceneManager;

    [Inject]
    public PopupPauseController(PopupManager popupManager, SignalBus signalBus, SceneScriptManager sceneManager)
    {
        this.popupManager = popupManager;
        this.signalBus = signalBus;
        this.sceneManager = sceneManager;
    }

    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
    }
    void ISceneReady.ReadyScene()
    {
        popupManager.OnPopupChanged += PauseHandle;
    }   

    void ISceneFinish.FinishScene()
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
        sceneManager.ResumeGame();
        Time.timeScale = 1;
    }
}
