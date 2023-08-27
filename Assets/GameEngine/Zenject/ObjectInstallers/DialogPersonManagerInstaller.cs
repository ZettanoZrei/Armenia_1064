using Assets.Game.HappeningSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DialogPersonManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<DialogPersonAllocator>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DialogPersonFabrica>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DialogPersonFocus>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DialogPersonKeeper>().FromComponentInHierarchy().AsSingle();
        Container.Bind<FigurePlaceFactory>().FromComponentInHierarchy().AsSingle();
    }
}
