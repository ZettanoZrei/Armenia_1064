using Assets.GameEngine.Zenject;
using Assets.Modules.UI;
using Assets.Systems.Zenject;
using Loader;
using UnityEditor;
using Zenject;

public class MonoMenuSceneInstaller : MonoInstaller
{
    public SimpleButton _continue;
    public SimpleButton newGame;
    public SimpleButton settings;
    public SimpleButton exit;
    public SimpleButton encyclopedia;
    public override void InstallBindings()
    {
        Container.Bind<SimpleButton>().WithId("continue").FromInstance(_continue).AsCached();
        Container.Bind<SimpleButton>().WithId("newGame").FromInstance(newGame).AsCached();
        Container.Bind<SimpleButton>().WithId("settings").FromInstance(settings).AsCached();
        Container.Bind<SimpleButton>().WithId("exit").FromInstance(exit).AsCached();
        Container.Bind<SimpleButton>().WithId("encyclopedia").FromInstance(encyclopedia).AsCached();

        Container.BindInterfacesTo<MenuController>().AsSingle();
        //Container.BindInterfacesTo<ClearOldData>().AsSingle();  //TODO: убрать это?
        Container.BindSceneScriptSystem();
    }
}
