using Assets.GameEngine.LoadTasks.Core;
using Loader;
using Zenject;

public class MonoLoadSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindLoadTasks();
    }

    private void BindLoadTasks()
    {
        Container.Bind<LoadTaskManager>().AsSingle();
        Container.BindInterfacesTo<TaskLoadHappenings>().AsTransient();
        Container.BindInterfacesTo<TaskCutText>().AsTransient();
        Container.BindInterfacesTo<TaskStartIntro>().AsTransient();
        Container.BindInterfacesTo<TaskLaunchGame>().AsTransient();

    }
}

