using Entities;
using GameSystems;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    //запускается при выходе из лагеря, если это тревел сцена. Нужен для запуска событий при выходе из лагеря. Требуется приоритет 4
    class LaunchComeOutFromCamp : IInitializable, ILateDisposable
    {
        private readonly MySceneManager sceneManager;
        //private readonly HappeningManager happeningManager;

        public LaunchComeOutFromCamp(MySceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        void IInitializable.Initialize()
        {
            sceneManager.OnChangeScene_Post += CheckCampScene;
        }
        void ILateDisposable.LateDispose()
        {
            sceneManager.OnChangeScene_Post -= CheckCampScene;
            sceneManager.OnChangeScene_Post -= WaitForTravelScene;
        }
        private void CheckCampScene(Scene scene)
        {
            if (scene == Scene.CampScene)
            {
                sceneManager.OnChangeScene_Post += WaitForTravelScene;
            }
        }

        private async void WaitForTravelScene(Scene scene)
        {
            sceneManager.OnChangeScene_Post -= WaitForTravelScene;
            if (sceneManager.IsTravelScene(scene))
            {
                await DelayLaunch();
            }
        }

        //костыль, проблема с изменениям коллекции в gamesystem
        async Task DelayLaunch()
        {
            await Task.Delay(100);
            if (sceneManager.IsTravelScene(sceneManager.CurrentScene))
            {
                var priority = 4;
                //happeningManager.LaunchHappenFromQueue(priority);
            }
        }

        
    }
}
