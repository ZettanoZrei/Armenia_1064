using Assets.Game;
using Assets.GameEngine.Menu.Settings;
using Zenject;

public class MonoSettingsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<SettingController>().FromComponentInHierarchy().AsCached();
    }
}