using Assets.Game;
using Assets.Game.Plot.Core;
using Assets.Game.Tutorial.Observers;
using Assets.GameEngine.Zenject;
using Assets.Systems.Zenject;
using GameSystems;
using Zenject;

public class MonoPlotSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<ObserverIsActive>().FromComponentsInHierarchy().AsTransient();
        Container.BindInterfacesTo<PlotObserver2>().FromComponentsInHierarchy().AsTransient();

        Container.BindPopupSystem();
        Container.BindSceneScriptSystem();
        //CommonInstaller.Install(Container);
    }
}
