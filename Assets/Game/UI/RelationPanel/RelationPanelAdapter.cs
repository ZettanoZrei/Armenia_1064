using Assets.Game.HappeningSystem.Persons;
using GameSystems;
using System;
using UniRx;
using UnityEngine;
using Zenject;

class RelationPanelAdapter : MonoBehaviour, IGameReadyElement, IGameFinishElement, IGameChangeSceneElement
{
    [SerializeField] private RelationPanelView panelView;
    private RelationManager relationManager;
    private CompositeDisposable disposable = new CompositeDisposable();
    [Inject]
    private void Construct(RelationManager relationManager)
    {
        this.relationManager = relationManager;
    }

    void IGameReadyElement.ReadyGame()
    {
        foreach(var relation in relationManager)
        {
            panelView.SetLevel(relation.Name, relation.Value.Value);
            relation.Value.Subscribe(value => panelView.SetLevel(relation.Name, value)).AddTo(disposable);
        }
    }

    void IGameFinishElement.FinishGame()
    {
        disposable.Clear();
    }

    void IGameChangeSceneElement.ChangeScene()
    {
        disposable.Clear();
    }
}
