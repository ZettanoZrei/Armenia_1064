using System;
using UnityEngine;

public class MoveMechanics : MonoBehaviour
{
    public event Action OnMovingEvent;
    public event Action OnFinishMovingEvent;
    public Vector2 Position => transform.position;
    public bool IsMoving => isMoving;

    private bool isMoving;


    public void Move()
    {
        if (!this.isMoving)
        {
            this.isMoving = true;
            OnMovingEvent?.Invoke();
        }
    }

    public void Stop()
    {
        if (this.isMoving)
        {
            this.isMoving = false;
            OnFinishMovingEvent?.Invoke();
        }
    }
}
