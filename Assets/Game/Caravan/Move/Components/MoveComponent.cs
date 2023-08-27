using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour, IMoveComponent
{
    [SerializeField]
    private MoveMechanics moveMechanics;

    public Vector2 Position => moveMechanics.Position;

    public event Action OnMovingEvent
    {
        add { moveMechanics.OnMovingEvent += value; }
        remove { moveMechanics.OnMovingEvent -= value; }
    }
    public event Action OnFinishMovingEvent
    {
        add { moveMechanics.OnFinishMovingEvent += value; }
        remove { moveMechanics.OnFinishMovingEvent -= value; }

    }

    public void Move()
    {
        moveMechanics.Move();
    }

    public void Stop()
    {
        moveMechanics.Stop();
    }
}
