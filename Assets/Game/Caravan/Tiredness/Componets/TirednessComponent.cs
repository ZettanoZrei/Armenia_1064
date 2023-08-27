using System;
using UnityEngine;

class TirednessComponent : MonoBehaviour, ITirednessComponent
{
    [SerializeField]
    private TirednessMechanics tirednessMechanics;

    public event Action<float> OnDecreaseStamina
    {
        add { tirednessMechanics.OnDecreaseStamina += value; }
        remove { tirednessMechanics.OnDecreaseStamina -= value; }
    }

    public void DoTired()
    {
        tirednessMechanics.DoTired();
    }

    public void StopTired()
    {
        tirednessMechanics.StopTired();
    }
}

