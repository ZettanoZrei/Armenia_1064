using UnityEngine;

public abstract class State : MonoBehaviour
{
    public virtual void EnterState()
    { }
    public virtual void ExitState()
    { }
    public virtual void UpdateState(float delta)
    { }

}


