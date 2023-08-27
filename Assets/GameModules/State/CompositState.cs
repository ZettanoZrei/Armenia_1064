using UnityEngine;
using System.Collections.Generic;

public class CompositState : State
{
    [SerializeField]
    private List<State> states = new List<State>();

    public override void EnterState()
    {
        states.ForEach(x => x.EnterState());
    }

    public override void ExitState()
    {
        states.ForEach(x => x.ExitState());
    }

    public override void UpdateState(float delta)
    {
        states.ForEach(x => x.UpdateState(delta));
    }
}


