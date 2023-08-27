using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.GameEngine
{
    public class GameSystemDIController : IInitializable, ILateDisposable
    {
        private readonly MySceneManager sceneManager;
        private readonly List<ILeaveGameComponentDI> leaveGames = new List<ILeaveGameComponentDI>();

        private bool isGameLoaded;
        public GameSystemDIController(MySceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }
        void IInitializable.Initialize()
        {
            sceneManager.OnChangeScene_Post += PerformLeaveGame;
        }
        void ILateDisposable.LateDispose()
        {
            sceneManager.OnChangeScene_Post -= PerformLeaveGame;
        }

        public void AddComponent(IComponentDI component)
        {
            if (component is ILeaveGameComponentDI leaveGameDI)
            {
                leaveGames.Add(leaveGameDI);
            }
        }
        private void PerformLeaveGame(Scene scene)
        {
            if (!isGameLoaded)
            {
                isGameLoaded = true;
                return;
            }

            if (scene == Scene.MainMenuScene)
            {
                foreach (var component in leaveGames)
                    component.LeaveGame();
            }
        }
    }
}
