using Zenject;
using System.ComponentModel;
using Entities;
using UnityEngine;
using Assets.Systems.Zenject;
using Assets.Game.Camp.Background;
using Assets.Game.Camp;
using Assets.Game.HappeningSystem;
using Assets.Game.Camp.IconsSystem;
using Assets.Game.HappeningSystem.View.Advice;
using Assets.Game.Timer;
using Assets.Modules.UI;
using Assets.Modules;
using Assets.Game.UI.TimeUI;
using Assets.Game.Parameters.EndedParamSystem;
using Assets.Game.InputSystem;
using Assets.GameEngine.Zenject;
using UnityEditor.IMGUI.Controls;
using UnityEditor;
using Assets.Game.Menu;
using ExtraInjection;

public class MonoCampSceneInstaller : MonoInstaller
{
    [SerializeField] private GameObject campIconPrefab;
    [SerializeField] private SimpleButton restButton;
    [SerializeField] private SimpleButton leaveButton;
    [SerializeField] private Transform backContainer;
    [SerializeField] private SimpleButton menu;

    public override void InstallBindings()
    {
        CommonInstaller.Install(Container);
        Container.BindInterfacesTo<LaunchComeInCampQuest>().AsTransient();
        BindRestMechanics();


        //UI
        Container.Bind<SimpleButton>().WithId("menu").FromInstance(menu).AsCached();
        Container.Bind<SimpleButton>().WithId("restButton").FromInstance(restButton).AsCached();
        Container.Bind<SimpleButton>().WithId("leaveButton").FromInstance(leaveButton).AsCached();
        Container.Bind<Transform>().WithId("backContainer").FromInstance(backContainer).AsCached();
        Container.Bind<ParamsWidget>().FromComponentInHierarchy().AsCached();
        Container.Bind<PeopleWidget>().FromComponentInHierarchy().AsCached();
        Container.Bind<RelationPanelView>().FromComponentInHierarchy().AsCached();
        Container.Bind<TimeView>().FromComponentInHierarchy().AsCached();


        //adapters
        Container.BindInterfacesTo<PeopleWidgetAdapter>().AsSingle();
        Container.BindInterfacesTo<ParamWidgetAdapter>().AsSingle();
        Container.BindInterfacesTo<RelationPanelAdapter>().AsSingle();

        //Controllers
        Container.BindInterfacesTo<RestController>().AsSingle();
        Container.BindInterfacesTo<LeaveController>().AsSingle();
        Container.BindInterfacesTo<IconController>().AsSingle();
        Container.BindInterfacesTo<TimeAdapter>().AsSingle();
        Container.BindInterfacesTo<TimeRestController>().AsSingle();
        Container.BindInterfacesTo<EndedParamController>().AsSingle();
        Container.BindInterfacesTo<InputController>().AsSingle();
        Container.BindInterfacesTo<PopupPauseController>().AsSingle();
        Container.BindInterfacesTo<CampIconController>().AsSingle();


        Container.BindInterfacesAndSelfTo<CampBackgroundManager>().AsSingle();
        

        Container.BindFactory<bool, string, CampIcon, CampIcon.Factory>()
                    .FromComponentInNewPrefab(campIconPrefab)
                    .UnderTransform(Container.Resolve<PortaitHeap>().transform);
        Container.Bind<IconsFabrica>().AsSingle();

       
        Container.BindPopupSystem();
    }

    private void BindRestMechanics()
    {
        Container.Bind<RestModel>().AsSingle();
        Container.Bind<NightWorkAdapter>().AsSingle();
        Container.Bind<NightWorkModel>().AsSingle();
        Container.Bind<RestManager>().AsSingle();
        Container.Bind<RestContext>().AsSingle();
        BindRestStepsQueue();
    }
    private void BindRestStepsQueue()
    {
        Container.Bind<IRestStep>().To<StepShowNightWorkPopup>().AsTransient();
        Container.Bind<IRestStep>().To<StepDoRest>().AsTransient();
        Container.Bind<IRestStep>().To<StepDarkTimePopup>().AsTransient();
        Container.Bind<IRestStep>().To<StepShowRestFinishPopup>().AsTransient();
    }
}
