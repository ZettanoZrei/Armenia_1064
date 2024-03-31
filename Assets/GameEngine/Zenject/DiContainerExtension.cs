using Assets.Game.HappeningSystem;
using Assets.Game.Message;
using Zenject;
using Assets.Game.Parameters.EndedParamSystem;
using Assets.Game.Parameters;
using Assets.Game;
using Assets.Modules;
using Assets.GameEngine.LoadTasks.Core;
using Assets.GameEngine.LoadTasks;
using Loader;
using System.ComponentModel;
using Assets.Game.Intro.Step;

namespace Assets.GameEngine.Zenject
{
    public static class DiContainerExtension 
    {
        public static void BindHappeningSystem(this DiContainer Container)
        {
            Container.BindInterfacesAndSelfTo<MessageManager>().AsSingle();
            Container.BindInterfacesTo<LaunchComeOutFromCamp>().AsSingle();
            //Container.BindCustomHappenManager();
        }

        public static void BindEndingParamSystem(this DiContainer Container) //TODO Перенести обратно на глобал? Данные с него должны сохраняться 
        {
            Container.BindInterfacesAndSelfTo<ParameterEndedObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndedParamMechanics>().AsSingle();
            Container.BindInterfacesTo<EndedParamViewHandler>().AsSingle();
            Container.BindInterfacesTo<EndedParamRemovingPeopleHandler>().AsSingle();
        }

        public static void BindPopupSystem(this DiContainer Container)
        {
            Container.Bind<PopupManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<PopupFabrica>().AsSingle();
            Container.Bind<PopupCatalog>().FromScriptableObjectResource("Entities/PopupCatalog").AsSingle();
            Container.BindInterfacesAndSelfTo<PopupContainer>().FromComponentInHierarchy().AsCached();    
            Container.Bind<BlockCurtain>().FromComponentInHierarchy().AsCached();
            Container.Bind<ShowUIElementsModel>().AsTransient();
        }

        public static void BindSceneScriptSystem(this DiContainer Container)
        {
            Container.Bind<SceneScriptManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScriptContext>().AsSingle();
            Container.BindInterfacesTo<SceneScriptController>().FromComponentInHierarchy().AsCached();
        }

        public static void BindLoadTasks(this DiContainer Container)
        {
            //Container.BindInterfacesTo<LoadTaskManager>().AsSingle();
            //Container.Bind<LoadTaskSettings>().AsTransient();

            Container.BindInterfacesTo<TaskLaunchGame>().AsTransient();
            Container.BindInterfacesTo<TaskStartIntro>().AsTransient();            
        }
    }
}
