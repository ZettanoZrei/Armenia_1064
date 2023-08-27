using Assets.Game.Parameters;
using Entities;
using GameSystems;
using Model.Types;
using Parameters;
using UniRx;
using UnityEngine;
using Zenject;

public class ParamWidgetAdapter : MonoBehaviour,
    IGameReadyElement, IGameFinishElement
{
    [SerializeField] private ParamsWidget paramsWidget;
    [Inject] private ParametersManager parameters;

    private CompositeDisposable disposable = new CompositeDisposable();
    void IGameReadyElement.ReadyGame()
    {
        paramsWidget.AddImmediately(ParameterType.Stamina, parameters.Stamina.Value);
        paramsWidget.AddImmediately(ParameterType.Spirit, parameters.Spirit.Value);
        paramsWidget.AddImmediately(ParameterType.Food, parameters.Food.Value);

        parameters.Stamina.Subscribe(_ => SetUIStamina(_)).AddTo(disposable);
        parameters.Spirit.Subscribe(_ => SetUISpirit(_)).AddTo(disposable);
        parameters.Food.Subscribe(_ => SetUIFood(_)).AddTo(disposable);

        //parameters.Stamina.Where(_ => _ < 5).Subscribe(_ => SetUIStamina(_, true)).AddTo(disposable);
    }


    void IGameFinishElement.FinishGame()
    {
        disposable.Clear();
    }


    private void SetUIFood(float value)
    {
        paramsWidget.SetFood(value);
    }

    private void SetUISpirit(float value)
    {
        paramsWidget.SetSpirit(value);
    }

    private void SetUIStamina(float value)
    {
        paramsWidget.SetStamina(value);
    }
}
