using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FiniteStateMachine<T> : MonoBehaviour, ISerializationCallbackReceiver where T : Enum
{
    [SerializeField] private List<StateInfo> states = new List<StateInfo>();

    private Dictionary<T, State> statesMap = new Dictionary<T, State>();

    [SerializeField]
    private T startState;

    private State currentState;

    private float deltaTime;


    private void Start()
    {
        deltaTime = Time.fixedDeltaTime;
        if (currentState == null)
        {
            if (statesMap.ContainsKey(startState))
                currentState = statesMap[startState];
        }
    }

    private void FixedUpdate()
    {
        StateUpdate();
    }

    public void EnterState(T stateType)
    {
        if (currentState != null)
            currentState.ExitState();


        if (statesMap.TryGetValue(stateType, out State state))
        {
            currentState = state;
            currentState.EnterState();
        }
    }

    public void StateUpdate()
    {
        if (currentState != null)
            currentState.UpdateState(deltaTime);
    }

    public void OnAfterDeserialize()
    {
        foreach (var state in states)
        {
            statesMap[state.stateType] = state.state;
        }
    }

    public void OnBeforeSerialize()
    {

    }

    [Serializable]
    public struct StateInfo
    {
        public T stateType;
        public State state;
    }
}
