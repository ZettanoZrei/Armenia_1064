using Assets.Game.Configurations;
using Model.Entities.Answers;
using Model.Types;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ParamsWidget : MonoBehaviour
{
    [SerializeField]
    private ProgressBar food;

    [SerializeField]
    private ProgressBar stamina;

    [SerializeField]
    private ProgressBar spirit;
    private ParametersConfig parametersConfig;

    [Inject]
    private void Construct(ParametersConfig parametersConfig)
    {
        this.parametersConfig = parametersConfig;
    }

    //костыль для мгновенного добавления старта игры
    public void AddImmediately(ParameterType type, float value)
    {
        float val = value / 100;
        switch (type)
        {
            case ParameterType.Food:
                food.SetProgress(val);
                break;
            case ParameterType.Spirit:
                spirit.SetProgress(val);
                break;
            case ParameterType.Stamina:
                stamina.SetProgress(val);
                break;
        }
    }

    public void SetSpirit(float value)
    {
        if (spirit != null)
        {
            float val = value / 100;
            if (CheckIfDiffIsSmall(spirit, val))
                spirit.SetProgress(val);
            else 
                SlowChanges(spirit, val);
        }
    }

    public void SetStamina(float value)
    {
        if (stamina != null)
        {
            float val = value / 100;
            if(CheckIfDiffIsSmall(stamina, val))
                stamina.SetProgress(val);
            else
                SlowChanges(stamina, val);
        }
    }
    public void SetFood(float value)
    {
        if (food != null)
        {
            float val = value / 100;
            if(CheckIfDiffIsSmall(food, val))
                food.SetProgress(val);
            else
                SlowChanges(food, val);
        }
    }

    private bool CheckIfDiffIsSmall(ProgressBar progressBar, float newValue)
    {
        return Mathf.Abs(newValue - progressBar.Value) < 0.05;
    }

    private void SlowChanges(ProgressBar progress, float newValue)
    {
        if (progress.Value > newValue)
        {
            StartCoroutine(SlowReduceEnumerator(progress, newValue));
        }
        else if (progress.Value < newValue)
        {
            StartCoroutine(SlowIncreaseEnumerator(progress, newValue));
        }
    }

    private IEnumerator SlowReduceEnumerator(ProgressBar progress, float newValue)
    {
        progress.SetSprite(parametersConfig.whiteProgress);
        while (true)
        {
            var tempValue = progress.Value - parametersConfig.changeSpeed;
            progress.SetProgress(tempValue);
            yield return new WaitForSecondsRealtime(0.01f);
            if (newValue >= progress.Value)          
                break;          
        }
        progress.SetDefault();
    }
    private IEnumerator SlowIncreaseEnumerator(ProgressBar progress, float newValue)
    {
        progress.SetSprite(parametersConfig.whiteProgress);
        while (true)
        {
            var tempValue = progress.Value + parametersConfig.changeSpeed;
            progress.SetProgress(tempValue);
            yield return new WaitForSecondsRealtime(0.01f);
            if (newValue <= progress.Value)
                break;
        }
        progress.SetDefault();
    }
}
