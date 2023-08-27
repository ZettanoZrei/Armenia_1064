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

public class MonoTravelSceneInstaller : MonoInstaller
{
    [SerializeField] private MonoEntity caravan;

    public override void InstallBindings()
    {
        Container.Bind<IEntity>().WithId("caravan").To<MonoEntity>().FromInstance(caravan).AsSingle();      
        Container.Bind<FiniteTriggerCatalog>().FromComponentInHierarchy().AsSingle();       
        Container.BindInterfacesTo<TravelSceneNavigatorController>().AsTransient();
        Container.Bind<NextTravelSceneTrigger>().FromComponentsInHierarchy().AsSingle();
        CommonInstaller.Install(Container);
    }    
}
