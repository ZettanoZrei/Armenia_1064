using Assets.Game;
using Cinemachine;
using GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PopupPauseController : MonoBehaviour, IGameReadyElement, IGameFinishElement
{
    private IGameSystem gameSystem;
    private PopupManager popupManager;

    [Inject]
    public void Construct(PopupManager popupManager, IGameSystem gameSystem)
    {
        this.popupManager = popupManager;
        this.gameSystem = gameSystem;
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
        gameSystem.PauseGame();
        Time.timeScale = 0;
    }

    private void Resume()
    {
        gameSystem.ResumeGame();
        Time.timeScale = 1;
    }
}
