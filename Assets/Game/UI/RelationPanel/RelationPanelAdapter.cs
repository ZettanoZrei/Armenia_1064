using Assets.Game.HappeningSystem.Persons;
using Assets.Modules;
using GameSystems.Modules;
using System;
using UniRx;
using UnityEngine;
using Zenject;

class RelationPanelAdapter : IInitializable, 
    ISceneReady, 
    ISceneFinish
{
    private readonly RelationPanelView panelView;
    private readonly SignalBus signalBus;
    private readonly RelationManager relationManager;
    private CompositeDisposable disposable = new CompositeDisposable();

    public RelationPanelAdapter(RelationManager relationManager, RelationPanelView panelView, SignalBus signalBus)
    {
        this.relationManager = relationManager;
        this.panelView = panelView;
        this.signalBus = signalBus;
    }
    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
    }
    void ISceneReady.ReadyScene()
    {
        foreach(var relation in relationManager)
        {
            panelView.SetLevel(relation.Name, relation.Value.Value);
            relation.Value.Subscribe(value => panelView.SetLevel(relation.Name, value)).AddTo(disposable);
        }
    }

    void ISceneFinish.FinishScene()
    {
        disposable.Clear();
    }   
}
