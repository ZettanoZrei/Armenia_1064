﻿using Assets.Game.Parameters;
using Assets.Modules;
using Entities;
using GameSystems.Modules;
using Parameters;
using UniRx;
using UnityEngine;
using Zenject;

public class PeopleWidgetAdapter : IInitializable,
    ISceneReady, 
    ISceneFinish
{
    private readonly PeopleWidget peopleWidget;
    private readonly ParametersManager parametersManager;

    private CompositeDisposable disposable = new CompositeDisposable();
    private readonly SignalBus signalBus;

    public PeopleWidgetAdapter(ParametersManager parametersManager, SignalBus signalBus, PeopleWidget peopleWidget)
    {
        this.signalBus = signalBus;
        this.parametersManager = parametersManager;
        this.peopleWidget = peopleWidget;
    }
    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
    }
    void ISceneReady.ReadyScene()
    {
        //SetUIPeople(parametersManager.People.Value);
        parametersManager.People
            .ObserveEveryValueChanged(x => x.Value)
            .Subscribe(_ => SetUIPeople(_))
            .AddTo(disposable);
    }
    void ISceneFinish.FinishScene()
    {
        disposable.Clear();
    }

    private void SetUIPeople(int value)
    {
        peopleWidget.SetPeople(value.ToString());
    }

    
}
