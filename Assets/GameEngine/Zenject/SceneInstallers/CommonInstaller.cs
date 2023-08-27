using Assets.Game;
using Assets.Game.HappeningSystem;
using Assets.Game.InputSystem;
using Assets.Game.Menu;
using Assets.Game.Plot.Core;
using Assets.Game.Tutorial.Observers;
using Assets.Game.UI.FailGameSystem;
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
            Container.BindInterfacesTo<ViewInjector>().FromComponentInHierarchy().AsCached();
            Container.Bind<MenuManager>().AsSingle();
            Container.Bind<MenuModel>().AsSingle();
            Container.Bind<IGameSystem>().To<GameSystem>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<KeyInputController>().AsSingle();
            Container.BindInterfacesTo<GameOverController>().AsSingle();
            
            Container.BindInterfacesTo<ObserverIsActive>().FromComponentsInHierarchy().AsTransient();
            Container.BindInterfacesTo<PlotObserver2>().FromComponentsInHierarchy().AsTransient();
        }        
    }
}
