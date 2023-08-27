using System;
using UnityEngine;
using Zenject;


public class TimerScript
{
    public event Action OnStarted;
    public event Action OnFinished;
    public event Action OnStop;
    public event Action OnElapsed;

    private float currentTime;
    private float duration;
    public TimerState State { get; set; }

    public bool IsLoop { get; set; } = true;
    public float Progress
    {
        get { return this.currentTime / this.duration; }
    }

    public float Duration
    {
        get { return this.duration; }
    }

    public float CurrentTime
    {
        get { return this.currentTime; }
    }


    public TimerScript(float duration)
    {
        this.duration = duration;
    }
    public TimerScript()
    {

    }

    /// <summary>
    /// run every frame
    /// </summary>
    public void Update()
    {
        if (State == TimerState.Playing)
        {
            this.currentTime += Time.deltaTime;
            if (currentTime > this.duration)
            {
                if (IsLoop)
                    this.currentTime = 0;
                OnElapsed?.Invoke();
            }
        }
    }

    public void Start()
    {
        if (State == TimerState.Playing)
        {
            return;
        }
        SetState(TimerState.Playing);
        this.OnStarted?.Invoke();
    }

    public void End()
    {
        if (State == TimerState.Finished)
        {
            return;
        }
        this.currentTime = 0;
        SetState(TimerState.Finished);
        this.OnFinished?.Invoke();
    }

    public void Stop()
    {
        if (State != TimerState.Playing)
        {
            return;
        }
        SetState(TimerState.Stoped);
        this.OnStop?.Invoke();
    }

    public void SetDuration(float sec)
    {
        this.duration = sec;
    }

    public void SetCurrentTime(float sec)
    {
        this.currentTime = sec;
    }

    private void SetState(TimerState state)
    {
        this.State = state;
    }
}
public enum TimerState
{
    Finished,
    Playing,
    Stoped
}

