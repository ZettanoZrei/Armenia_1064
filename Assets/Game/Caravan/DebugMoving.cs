using Assets.Game.Menu;
using Entities;
using PixelCrushers.DialogueSystem;
using System;
using UnityEngine;
using Zenject;

public class DebugMoving : MonoBehaviour
{
    [SerializeField] MonoEntity caravan;
    IMoveComponent moveComponent;

    [Inject] MenuManager menuManager;

    private void Start()
    {
        moveComponent = caravan.Element<IMoveComponent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            moveComponent.Stop();

        if (Input.GetKeyDown(KeyCode.LeftAlt))
            moveComponent.Move();

        if(Input.GetKeyDown(KeyCode.Q))
            menuManager.ShowMenu();

    }

    public void ShowResponse(Response[] responses)
    {
        Console.WriteLine("!!!");
    }
}
