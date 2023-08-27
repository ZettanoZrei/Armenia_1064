using Assets.Game.Configurations;
using Assets.Game.Parameters;
using System.Collections;
using UnityEngine;
using Zenject;

class TirednessMechHandler : MonoBehaviour
{
    [SerializeField]
    private TirednessMechanics tirednessMechanics;

    private bool isCoroutina;
    private CaravanConfig config;

    [Inject]
    public void Construct(CaravanConfig config)
    {
        this.config = config;
    }

    private void Update()
    {
        if (tirednessMechanics.IsTired)
        {
            if (isCoroutina)
                return;
            isCoroutina = true;
            StartCoroutine(TiredEnumerator());
        }
    }

    private IEnumerator TiredEnumerator()
    {
        yield return new WaitForSeconds(config.spendTime);
        tirednessMechanics.DecreaseStamina(-config.staminaSpend);
        isCoroutina = false;
    }
}
