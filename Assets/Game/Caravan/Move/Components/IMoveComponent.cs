using System;
using UnityEngine;

public interface IMoveComponent
{
    void Move();
    void Stop();
    Vector2 Position { get; }
    event Action OnMovingEvent;
    event Action OnFinishMovingEvent;
}
