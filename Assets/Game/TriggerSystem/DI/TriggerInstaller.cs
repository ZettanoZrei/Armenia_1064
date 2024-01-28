using Assets.Game.DialogBackTriggers;
using Assets.Game.HappeningSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TriggerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LaunchStaticTrigger>().FromComponentsInHierarchy().AsCached();
        Container.Bind<ActivatorStaticTrigger>().FromComponentsInHierarchy().AsCached();
        Container.Bind<FastPointerTrigger>().FromComponentsInHierarchy().AsCached();
        Container.Bind<CampQuestTriggerModel>().FromComponentsInHierarchy().AsCached();
        Container.Bind<DialogBackTrigger>().FromComponentsInHierarchy().AsCached();
        Container.Bind<CampIcon>().FromComponentsInHierarchy().AsCached();


        Container.BindInterfacesTo<FiniteTriggerCatalog>().FromComponentInHierarchy().AsCached();
    }
}
