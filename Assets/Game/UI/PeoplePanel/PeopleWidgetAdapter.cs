using Assets.Game.Parameters;
using Entities;
using GameSystems;
using Parameters;
using UniRx;
using UnityEngine;
using Zenject;

public class PeopleWidgetAdapter : MonoBehaviour,
    IGameReadyElement, IGameFinishElement, IGameChangeSceneElement
{
    [SerializeField]
    private PeopleWidget peopleWidget;

    [Inject]
    private ParametersManager parametersManager;

    private CompositeDisposable disposable = new CompositeDisposable();
    void IGameReadyElement.ReadyGame()
    {
        //SetUIPeople(parametersManager.People.Value);
        parametersManager.People
            .ObserveEveryValueChanged(x => x.Value)
            .Subscribe(_ => SetUIPeople(_))
            .AddTo(disposable);
    }
    void IGameFinishElement.FinishGame()
    {
        disposable.Clear();
    }
    void IGameChangeSceneElement.ChangeScene()
    {
        disposable.Clear();
    }
    private void SetUIPeople(int value)
    {
        peopleWidget.SetPeople(value.ToString());
    }


}
