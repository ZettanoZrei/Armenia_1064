using Assets.Game.Configurations;
using Assets.Game.HappeningSystem;
using Assets.Systems.SaveSystem;
using Model.Entities.Happenings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.GameSystems.SaveSystem
{
    internal class SaveController : ITickable, IInitializable, ILateDisposable
    {
        private readonly SaveManager saveManager;
        private readonly MySceneManager sceneManager;
        private readonly HappeningManager happeningManager;
        private readonly SaveConfing saveConfing;
        private readonly TimerScript timerScript;

        public SaveController(SaveManager saveManager, MySceneManager sceneManager, ConfigurationRuntime configurationRuntime, HappeningManager happeningManager)
        {
            this.saveManager = saveManager;
            this.sceneManager = sceneManager;
            this.happeningManager = happeningManager;
            this.saveConfing = configurationRuntime.SaveConfing;
            timerScript = new TimerScript(saveConfing.timeSave);
        }

        void IInitializable.Initialize()
        {
            sceneManager.OnChangeScene_Post += CheckScene;
            timerScript.OnElapsed += SaveAsync;

            //happeningManager.OnFinishHappening += Save;
        }



        void ILateDisposable.LateDispose()
        {
            //happeningManager.OnFinishHappening -= Save;
            sceneManager.OnChangeScene_Post -= CheckScene;
            timerScript.OnElapsed -= SaveAsync;
        }

        void ITickable.Tick()
        {
            if (timerScript.State == TimerState.Playing)
                timerScript.Update();
        }

        private async void SaveAsync()
        {
            if (CheckPermittedScenes(sceneManager.CurrentScene) && !saveConfing.IsPlotSegment)
                await saveManager.SaveAsync();
        }
        private void CheckScene(Scene scene)
        {
            if (!saveConfing.isSave)
                return;
            if (CheckPermittedScenes(scene))
                timerScript.Start();
            else
                timerScript.End();
        }

        private bool CheckPermittedScenes(Scene scene) 
        {
            return sceneManager.IsTravelScene(scene) || scene == Scene.CampScene;
        }
    }
}
