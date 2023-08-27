using Assets.Game.HappeningSystem;
using Entities;
using Zenject;
using UnityEngine;
using Assets.Systems.Zenject;
using Assets.Game.HappeningSystem.View.Advice;

public class MonoDialogSceneInstaller : MonoInstaller
{
    [SerializeField] private FigurePersonManager figurePersonManager;
    
    public override void InstallBindings()
    {
        CommonInstaller.Install(Container);

        Container.Bind<FigurePersonManager>().FromInstance(figurePersonManager).AsCached();
        Container.Bind<DialogBackgroundManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<DialogView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DialogPresenter>().FromComponentInHierarchy().AsCached();
        Container.Bind<DialogBackgroundView>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<ReactionPopupManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<ReactionPopupController>().AsSingle();        
    }
}