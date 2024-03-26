using Assets.DialogSystem.Scripts.Conclusion;
using Assets.DialogSystem.Scripts.UI;
using Assets.DialogSystem.Scripts;
using Assets.Game;
using Assets.Game.HappeningSystem;
using Assets.Game.InputSystem;
using Assets.Game.Menu;
using Assets.Game.Plot.Core;
using Assets.Game.SceneScripts;
using Assets.Game.Tutorial.Observers;
using Assets.Game.UI.FailGameSystem;
using Assets.GameEngine.Zenject;
using ExtraInjection;
using GameSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using PixelCrushers.DialogueSystem;

namespace Assets.Systems.Zenject
{
    internal class CommonInstaller : Installer<CommonInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MenuManager>().AsSingle();
            Container.Bind<MenuModel>().AsSingle();
            Container.BindInterfacesTo<ExtraInjector>().AsTransient();
            Container.BindInterfacesTo<KeyInputController>().AsSingle();
            Container.BindInterfacesTo<GameOverController>().AsSingle();
            
            Container.BindInterfacesTo<ObserverIsActive>().FromComponentsInHierarchy().AsTransient();
            Container.BindInterfacesTo<PlotObserver2>().FromComponentsInHierarchy().AsTransient();

            Container.BindInterfacesTo<SceneStartBeacon>().FromComponentInHierarchy().AsCached();
            Container.BindSceneScriptSystem();
            //TODO: необходимо придумать как заставить работать ExtraInject в объектах созданных через фабрики. Либо переделать эту систему
            Container.BindCustomHappenManager();

            NewDialogSistem();
        }

        private void NewDialogSistem() //todo refactoring
        {
            //Container.Bind<w.DialogueSystemController>().FromInstance(dialogueSystemController).AsCached();
            Container.BindInterfacesAndSelfTo<DialogConclusionManager>().AsSingle();
            Container.BindInterfacesTo<DialogController>().FromComponentInHierarchy().AsCached();
            Container.Bind<ActorEventObserver>().FromComponentInHierarchy().AsCached();
            Container.Bind<ConversaionActor>().FromComponentInHierarchy().AsCached();
            Container.Bind<StoryActor>().FromComponentInHierarchy().AsCached();

            Container.BindInterfacesAndSelfTo<DialogAgent>().AsTransient();
            Container.Bind<DialogConclusionAgent>().AsTransient();

            Container.BindFactory<string, string, DialogStarter, DialogStarter.Factory>();
            Container.BindFactory<LeaveCampStarter, LeaveCampStarter.Factory>();
            Container.BindFactory<int, SetupCampStarter, SetupCampStarter.Factory>();
        }
        
    }
}
