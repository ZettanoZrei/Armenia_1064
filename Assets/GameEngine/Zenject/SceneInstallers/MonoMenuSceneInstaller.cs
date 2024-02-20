using Assets.Game.SceneScripts;
using Assets.Game.UI.DebugLoading;
using Assets.GameEngine.Zenject;
using Assets.Modules.UI;
using Assets.Systems.Zenject;
using ExtraInjection;
using Loader;
using UnityEditor;
using UnityEngine;
using Zenject;

public class MonoMenuSceneInstaller : MonoInstaller
{
    public SimpleButton _continue;
    public SimpleButton newGame;
    public SimpleButton settings;
    public SimpleButton exit;
    public SimpleButton encyclopedia;

    public LoadGameButton prefab;
    public Transform loadGameButtonPrefab;
    public override void InstallBindings()
    {
        Container.Bind<SimpleButton>().WithId("continue").FromInstance(_continue).AsCached();
        Container.Bind<SimpleButton>().WithId("newGame").FromInstance(newGame).AsCached();
        Container.Bind<SimpleButton>().WithId("settings").FromInstance(settings).AsCached();
        Container.Bind<SimpleButton>().WithId("exit").FromInstance(exit).AsCached();
        Container.Bind<SimpleButton>().WithId("encyclopedia").FromInstance(encyclopedia).AsCached();
        Container.BindInterfacesTo<SceneStartBeacon>().FromComponentInHierarchy().AsCached();
        Container.BindInterfacesTo<MenuController>().AsSingle();
        Container.BindInterfacesTo<ExtraInjector>().AsSingle();
        Container.BindInterfacesTo<ClearOldData>().AsSingle();  //TODO: убрать это?
        Container.BindSceneScriptSystem();

        //debug
        Container.BindFactory<LoadGameButton, LoadGameButton.Factory>()
           .FromComponentInNewPrefab(prefab)
           .UnderTransform(loadGameButtonPrefab);
    }
}
