using Assets.Game.Parameters;
using Assets.Modules;
using Entities;
using GameSystems.Modules;
using Model.Types;
using Parameters;
using UniRx;
using UnityEngine;
using Zenject;

public class ParamWidgetAdapter : IInitializable,
    ISceneReady, 
    ISceneFinish
{
    private readonly ParamsWidget paramsWidget;
    private readonly SignalBus signalBus;
    private readonly ParametersManager parameters;

    private CompositeDisposable disposable = new CompositeDisposable();

    public ParamWidgetAdapter(ParametersManager parameters, ParamsWidget paramsWidget, SignalBus signalBus)
    {
        this.parameters = parameters;
        this.paramsWidget = paramsWidget;
        this.signalBus = signalBus;
    }

    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
    }

    void ISceneReady.ReadyScene()
    {
        paramsWidget.AddImmediately(ParameterType.Stamina, parameters.Stamina.Value);
        paramsWidget.AddImmediately(ParameterType.Spirit, parameters.Spirit.Value);
        paramsWidget.AddImmediately(ParameterType.Food, parameters.Food.Value);

        parameters.Stamina.Subscribe(_ => SetUIStamina(_)).AddTo(disposable);
        parameters.Spirit.Subscribe(_ => SetUISpirit(_)).AddTo(disposable);
        parameters.Food.Subscribe(_ => SetUIFood(_)).AddTo(disposable);

        //parameters.Stamina.Where(_ => _ < 5).Subscribe(_ => SetUIStamina(_, true)).AddTo(disposable);
    }


    void ISceneFinish.FinishScene()
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
