using System;
using UnityEngine;

class TirednessMechanics : MonoBehaviour
{
    public event Action OnBeginTiredEvent;
    public event Action OnFinishTiredEvent;

    public event Action<float> OnDecreaseStamina;
    public Vector2 Position => transform.position;
    public bool IsTired => isTired;

    private bool isTired;

    
    public void DecreaseStamina(float minusStamina)
    {
        OnDecreaseStamina?.Invoke(minusStamina);
    }

    public void DoTired()
    {
        if (!this.isTired)
        {
            this.isTired = true;
            OnBeginTiredEvent?.Invoke();
        }
    }

    internal void StopTired()
    {
        if (this.isTired)
        {
            this.isTired = false;
            OnFinishTiredEvent?.Invoke();
        }
    }  
}
