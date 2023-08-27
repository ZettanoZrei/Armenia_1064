using Assets.Game;
using Assets.Game.Plot.Core;
using Assets.Game.Tutorial.Observers;
using Assets.Systems.Zenject;
using GameSystems;
using Zenject;

public class MonoPlotSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IGameSystem>().To<GameSystem>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<ViewInjector>().FromComponentInHierarchy().AsCached();
        Container.BindInterfacesTo<ObserverIsActive>().FromComponentsInHierarchy().AsTransient();
        Container.BindInterfacesTo<PlotObserver2>().FromComponentsInHierarchy().AsTransient();
        //CommonInstaller.Install(Container);
    }
}
