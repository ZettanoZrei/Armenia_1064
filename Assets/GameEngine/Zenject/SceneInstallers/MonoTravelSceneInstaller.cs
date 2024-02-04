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

public class MonoTravelSceneInstaller : MonoInstaller
{
    [SerializeField] private MonoEntity caravan;
    [SerializeField] private SimpleButton setupCampButton;
    [SerializeField] private SimpleButton menu;
    [SerializeField] private TimeView timeView;
    [SerializeField] private ParamsWidget paramsWidget;
    [SerializeField] private PeopleWidget peopleWidget;
    [SerializeField] private RelationPanelView relationPanelView;
    public override void InstallBindings()
    {
        CommonInstaller.Install(Container);
        Container.Bind<IEntity>().WithId("caravan").To<MonoEntity>().FromInstance(caravan).AsSingle();      
        Container.Bind<FiniteTriggerCatalog>().FromComponentInHierarchy().AsSingle();       
        Container.BindInterfacesTo<TravelSceneNavigatorController>().AsTransient();
        Container.Bind<NextTravelSceneTrigger>().FromComponentsInHierarchy().AsSingle();


        //UI
        Container.Bind<SimpleButton>().WithId("setupCampButton").FromInstance(setupCampButton).AsCached();
        Container.Bind<SimpleButton>().WithId("menu").FromInstance(menu).AsCached();
        Container.Bind<TimeView>().FromInstance(timeView).AsCached();
        Container.Bind<ParamsWidget>().FromInstance(paramsWidget).AsCached();
        Container.Bind<PeopleWidget>().FromInstance(peopleWidget).AsCached();
        Container.Bind<RelationPanelView>().FromInstance(relationPanelView).AsCached();

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

        
        //Triggers
        Container.Bind<FiniteTriggerCatalog>().FromComponentInHierarchy().AsCached();

        Container.BindSceneScriptSystem();     
    }    
}
