using Assets.Game.HappeningSystem;
using Entities;
using Zenject;
using UnityEngine;
using Assets.Systems.Zenject;
using Assets.Game.HappeningSystem.View.Advice;
using Assets.Game.Parameters.EndedParamSystem;
using Assets.Game.InputSystem;

public class MonoDialogSceneInstaller : MonoInstaller
{
    [SerializeField] private FigurePersonManager figurePersonManager;
    [SerializeField] private ParamsWidget paramsWidget;
    [SerializeField] private PeopleWidget peopleWidget;
    [SerializeField] private RelationPanelView relationPanelView;
    public override void InstallBindings()
    {
        CommonInstaller.Install(Container);

        Container.Bind<FigurePersonManager>().FromInstance(figurePersonManager).AsCached();
        Container.Bind<DialogBackgroundManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<DialogView>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<DialogPresenter>().FromComponentInHierarchy().AsCached();
        Container.Bind<DialogBackgroundView>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<ReactionPopupManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<ReactionPopupController>().AsSingle();


        Container.BindInterfacesTo<EndedParamController>().AsSingle();
        Container.BindInterfacesTo<InputController>().AsSingle();
        Container.BindInterfacesTo<PopupPauseController>().AsSingle();

        //ui
        Container.Bind<ParamsWidget>().FromInstance(paramsWidget).AsCached();
        Container.Bind<PeopleWidget>().FromInstance(peopleWidget).AsCached();
        Container.Bind<RelationPanelView>().FromInstance(relationPanelView).AsCached();

    }
}