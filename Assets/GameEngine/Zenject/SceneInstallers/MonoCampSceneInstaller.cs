using Zenject;
using System.ComponentModel;
using Entities;
using UnityEngine;
using Assets.Systems.Zenject;
using Assets.Game.Camp.Background;
using Assets.Game.Camp;
using Assets.Game.Parameters;
using Assets.Systems.SaveSystem;
using Assets.Game.HappeningSystem;
using Assets.Game.Camp.IconsSystem;
using Assets.Game.HappeningSystem.View.Advice;
using Assets.Game.Timer;

public class MonoCampSceneInstaller : MonoInstaller
{
    [SerializeField] private GameObject campIconPrefab;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CampBackgroundManager>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<LaunchComeInCampQuest>().AsTransient();
        BindRestMechanics();
        CommonInstaller.Install(Container);
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
