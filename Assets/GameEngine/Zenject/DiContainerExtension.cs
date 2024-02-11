using Assets.Game.HappeningSystem.AfterHappenAction;
using Assets.Game.HappeningSystem.Happenings;
using Assets.Game.HappeningSystem;
using Assets.Game.Message;
using Zenject;
using Assets.Game.HappeningSystem.ManagementHappens;
using Assets.Game.Parameters.EndedParamSystem;
using Assets.Game.Parameters;
using Assets.Game;
using Assets.Modules;

namespace Assets.GameEngine.Zenject
{
    public static class DiContainerExtension 
    {
        public static void BindHappeningSystem(this DiContainer Container)
        {
            Container.Bind<HappeningManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<HappeningLauncher>().AsSingle();
            Container.Bind<AfterActionManager>().AsSingle();
            Container.Bind<HappeningModel>().AsTransient();
            Container.Bind<DialogModelDecorator>().AsSingle();
            Container.Bind<AccidentPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<MessageManager>().AsSingle();
            Container.Bind<HappeningReplaceManager>().AsSingle();
            Container.BindInterfacesTo<LaunchComeOutFromCamp>().AsSingle();
            Container.BindInterfacesTo<ConsequencesController>().AsSingle();
            Container.Bind<ConsequencesHandler>().AsSingle();
            //Container.BindCustomHappenManager();
        }

        public static void BindCustomHappenManager(this DiContainer Container)
        {
            Container.Bind<ManagerTypeResolver>().AsSingle();
            Container.BindFactory<DialogManager, DialogManager.Factory>();
            Container.BindFactory<AccidentManager, AccidentManager.Factory>();
            Container.BindFactory<IHappeningManager, HappeningManagerFactory>().FromFactory<CustomManagerFactory>();
        }
        public static void BindEndingParamSystem(this DiContainer Container)
        {
            Container.BindInterfacesAndSelfTo<ParameterEndedObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndedParamMechanics>().AsSingle();
            Container.BindInterfacesTo<EndedParamViewHandler>().AsSingle();
            Container.BindInterfacesTo<EndedParamRemovingPeopleHandler>().AsSingle();
        }

        public static void BindPopupSystem(this DiContainer Container)
        {
            Container.Bind<PopupManager>().AsSingle();
            Container.Bind<PopupFabrica>().AsSingle();
            Container.Bind<PopupCatalog>().FromScriptableObjectResource("Entities/PopupCatalog").AsSingle();
            Container.Bind<PopupContainer>().FromComponentInHierarchy().AsCached();    
            Container.Bind<BlockCurtain>().FromComponentInHierarchy().AsCached();    
        }

        public static void BindSceneScriptSystem(this DiContainer Container)
        {
            Container.Bind<SceneScriptManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScriptContext>().AsSingle();
            Container.BindInterfacesTo<SceneScriptController>().FromComponentInHierarchy().AsCached();
        }
    }
}
