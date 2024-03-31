using Assets.Game.HappeningSystem;
using Entities;
using Zenject;
using UnityEngine;
using Assets.Systems.Zenject;
using Assets.Game.HappeningSystem.View.Advice;
using Assets.Game.Parameters.EndedParamSystem;
using Assets.Game.InputSystem;
using Assets.GameEngine.Zenject;
using Assets.Modules.UI;
using UnityEditor;

public class MonoDialogSceneInstaller : MonoInstaller
{
    [SerializeField] private FigurePersonManager figurePersonManager;
    [SerializeField] private SimpleButton menu;
    public override void InstallBindings()
    {
        CommonInstaller.Install(Container);

        Container.Bind<FigurePersonManager>().FromInstance(figurePersonManager).AsCached();
        Container.Bind<DialogBackgroundManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<DialogView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DialogBackgroundView>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<ReactionPopupManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<ReactionPopupController>().AsSingle();


        Container.BindInterfacesTo<EndedParamController>().AsSingle();
        Container.BindInterfacesTo<InputController>().AsSingle();
        Container.BindInterfacesTo<PopupPauseController>().AsSingle();

        //ui
        Container.Bind<ParamsWidget>().FromComponentInHierarchy().AsCached();
        Container.Bind<PeopleWidget>().FromComponentInHierarchy().AsCached();
        Container.Bind<RelationPanelView>().FromComponentInHierarchy().AsCached();
        Container.BindPopupSystem();
        Container.Bind<SimpleButton>().WithId("menu").FromInstance(menu).AsCached();

        //adapter
        Container.BindInterfacesTo<ParamWidgetAdapter>().AsSingle();
    }
}