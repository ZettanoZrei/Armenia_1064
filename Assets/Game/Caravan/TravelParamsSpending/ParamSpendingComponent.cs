using Assets.Game.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ParamSpendingComponent : MonoBehaviour
{
    [SerializeField]
    private ParamSpendingMechanics spendingMechanics;
    public void SpendParam()
    {
        spendingMechanics.SpendParam();
    }

    public void StopSpendParam()
    {
        spendingMechanics.StopSpend();
    }
}
