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

            Container.BindInterfacesTo<DialogsCnainController>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesTo<DialogResultManager>().FromComponentsInHierarchy().AsCached();
        }        
    }
}
