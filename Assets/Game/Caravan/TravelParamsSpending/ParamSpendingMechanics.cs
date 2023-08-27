using System;
using UnityEngine;

public class ParamSpendingMechanics : MonoBehaviour
{
    public bool IsSpend;
    
    public void SpendParam()
    {
        if (!IsSpend)
        {
            IsSpend = true;
        }
    }

    public void StopSpend()
    {
        if (IsSpend)
        {
            IsSpend = false;
        }
    }
}
