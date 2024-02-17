using Assets.Game.SceneScripts;
using Assets.Modules;
using ExtraInjection;
using Zenject;

public class MonoLoadSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<SceneStartBeacon>().FromComponentInHierarchy().AsCached();
        Container.Bind<SceneScriptManager>().AsSingle();
        Container.BindInterfacesTo<SceneScriptController>().FromComponentInHierarchy().AsCached();
        Container.BindInterfacesTo<ExtraInjector>().AsSingle();
    }
}

