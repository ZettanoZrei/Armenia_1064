using Assets.Game;
using GameSystems;
using UnityEngine;
using Zenject;

public class PopupBSClearController : MonoBehaviour, IGameChangeSceneElement
{
    [Inject] private PopupManager popupManager;
    void IGameChangeSceneElement.ChangeScene()
    {
        popupManager.ClearCache();
    }
}
