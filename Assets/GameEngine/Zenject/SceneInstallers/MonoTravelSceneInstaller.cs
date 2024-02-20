using Entities;
using Zenject;
using UnityEngine;
using Assets.Game.HappeningSystem;
using Assets.Systems.Zenject;
using System.ComponentModel;
using Assets.Systems.SaveSystem;
using Assets.Game.Camp;
using Assets.Game.Parameters;
using Assets.Game.Timer;
using Assets.Modules;
using Assets.Modules.UI;
using Parameters;
using Assets.Game.DialogBackTriggers;
using Assets.Game.Parameters.EndedParamSystem;
using Assets.Game.UI.TimeUI;
using Assets.Game.InputSystem;
using Assets.GameEngine.Zenject;
using ExtraInjection;

public class MonoTravelSceneInstaller : MonoInstaller
{
    [SerializeField] private MonoEntity caravan;
    [SerializeField] private SimpleButton setupCampButton;
    [SerializeField] private SimpleButton menu;

    public override void InstallBindings()
    {
        CommonInstaller.Install(Container);
        Container.Bind<IEntity>().WithId("caravan").To<MonoEntity>().FromInstance(caravan).AsSingle();        
        Container.BindInterfacesTo<TravelSceneNavigatorController>().AsTransient();
        Container.Bind<NextTravelSceneTrigger>().FromComponentsInHierarchy().AsSingle();

        //UI
        Container.Bind<SimpleButton>().WithId("setupCampButton").FromInstance(setupCampButton).AsCached();
        Container.Bind<SimpleButton>().WithId("menu").FromInstance(menu).AsCached();
        Container.Bind<TimeView>().FromComponentInHierarchy().AsCached();
        Container.Bind<ParamsWidget>().FromComponentInHierarchy().AsCached();
        Container.Bind<PeopleWidget>().FromComponentInHierarchy().AsCached();
        Container.Bind<RelationPanelView>().FromComponentInHierarchy().AsCached();

        //Adapters
        Container.BindInterfacesTo<TimeAdapter>().AsSingle();
        Container.BindInterfacesTo<PeopleWidgetAdapter>().AsSingle();
        Container.BindInterfacesTo<ParamWidgetAdapter>().AsSingle();
        Container.BindInterfacesTo<RelationPanelAdapter>().AsSingle();

        //Controllers
        Container.BindInterfacesTo<MoveController>().AsSingle();
        Container.BindInterfacesTo<TirednessCotroller>().AsSingle();
        Container.BindInterfacesTo<ParamSpendingController>().AsSingle();
        Container.BindInterfacesTo<TriggerController>().AsSingle();
        Container.BindInterfacesTo<EndedParamController>().AsSingle();
        Container.BindInterfacesTo<SetupCampController>().AsSingle();
        Container.BindInterfacesTo<TimeTravelController>().AsSingle();
        Container.BindInterfacesTo<InputController>().AsSingle();
        Container.BindInterfacesTo<PopupPauseController>().AsSingle();
        Container.BindInterfacesTo<CaravanRepositoryBSController>().AsSingle();
      
        Container.BindPopupSystem();
    }    
}
