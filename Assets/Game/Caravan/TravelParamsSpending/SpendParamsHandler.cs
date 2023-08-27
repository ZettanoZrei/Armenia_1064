using Assets.Game.Configurations;
using Assets.Game.Parameters;
using System.Collections;
using UnityEngine;
using Zenject;

public class SpendParamsHandler : MonoBehaviour
{
    [SerializeField] private ParamSpendingMechanics spendingMechanics;
    private CaravanConfig config;
    private ParametersManager parameterManager;
    private bool isCoroutina;

    [Inject]
    public void Construct(CaravanConfig config, ParametersManager parameterManager)
    {
        this.config = config;
        this.parameterManager = parameterManager;
    }

    private void Update()
    {
        if(spendingMechanics.IsSpend)
        {
            if (isCoroutina) return;
            isCoroutina = true;
            StartCoroutine(TiredEnumerator());
        }
    }

    private float CalculateSpendFoodValue(int people, float coef)
    {
        return coef * people;
    }

    private float CalculateSpendSpiritValue(int people, float coef)
    {
        return -coef * (people - 400);
    }
    private IEnumerator TiredEnumerator()
    {
        yield return new WaitForSeconds(config.spendTime);
        var spendFoodValue = CalculateSpendFoodValue(parameterManager.People.Value, config.foodSpendCoef);
        var spendSpiritValue = CalculateSpendSpiritValue(parameterManager.People.Value, config.spiritSpendCoef);
        parameterManager.ChangeFood(-spendFoodValue);
        parameterManager.ChangeSpirit(-spendSpiritValue);
        isCoroutina = false;
    }
}
