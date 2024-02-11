using Assets.Game.DialogBackTriggers;
using Assets.Game.HappeningSystem;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "TriggersInstaller", menuName = "Installers/TriggersInstaller")]
public class TriggersInstaller : ScriptableObjectInstaller<TriggersInstaller>
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
