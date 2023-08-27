using Assets.Game.HappeningSystem;
using System;
using UnityEngine;

public class NextTravelSceneTrigger : BaseFiniteTrigger
{
    [SerializeField] private Scene nextScenePointer;
    public event Action<Scene> OnNextScene;

    protected override void EnterCaravanAction()
    {
        IsDone = true;
        OnNextScene?.Invoke(nextScenePointer);
    }
}