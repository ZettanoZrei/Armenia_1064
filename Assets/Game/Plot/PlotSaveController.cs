using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.HappeningSystem;
using Assets.Game.Plot.Core;
using Assets.Systems.SaveSystem;
using Model.Entities.Happenings;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Assets.Game.Plot
{
    internal class PlotSaveController : IInitializable
    {
        private readonly PlotManager plotManager;
        private readonly SaveManager saveManager;
        private readonly MySceneManager sceneManager;
        private readonly SaveConfing saveConfing;
        private readonly PlotConfig plotConfig;
        private IEnumerable<PlotStepType> scenes = new List<PlotStepType> { PlotStepType.BeginGame, PlotStepType.FirstWords, PlotStepType.Dialog,
            PlotStepType.BeginAttack_2, PlotStepType.Storming, PlotStepType.GameTitle };
        public PlotSaveController(ConfigurationRuntime configurationRuntime, PlotManager plotManager, SaveManager saveManager, MySceneManager sceneManager)
        {
            saveConfing = configurationRuntime.SaveConfing;
            this.plotManager = plotManager;
            this.saveManager = saveManager;
            this.sceneManager = sceneManager;
            this.plotConfig = configurationRuntime.PlotConfig;
        }

        void IInitializable.Initialize()
        {
            if (!plotConfig.activate)
                return;

            saveConfing.IsPlotSegment = true;
            plotManager.OnShowStep += PlotManager_OnShowStep;
            //happeningManager.OnFinishHappening += HappeningManager_OnFinishHappening;
            sceneManager.OnChangeScene_Post += ComeInCastle;
            sceneManager.OnChangeScene_Post += ComeInTravelScene_1;

        }

        private async void ComeInTravelScene_1(Scene scene)
        {
            if (scene == Scene.Travel_1)
            {
                await saveManager.SaveAsync();
                FinishThisControllerWork();
            }
        }

        private void FinishThisControllerWork()
        {
            saveConfing.IsPlotSegment = false;
            plotManager.OnShowStep -= PlotManager_OnShowStep;
            //happeningManager.OnFinishHappening -= HappeningManager_OnFinishHappening;
            sceneManager.OnChangeScene_Post -= ComeInCastle;
            sceneManager.OnChangeScene_Post -= ComeInTravelScene_1;
        }

        private async void ComeInCastle(Scene scene)
        {
            if (scene == Scene.CampScene && sceneManager.PreviousScene != Scene.MainMenuScene)
            {
                sceneManager.OnChangeScene_Post -= ComeInCastle;
                await saveManager.SaveAsync();
            }
        }



        private async void HappeningManager_OnFinishHappening(Happening obj)
        {
            if (obj is DialogHappening && plotManager.CurrentStepIndex < (int)PlotStepType.BeginAttack_2)
                await saveManager.SaveAsync();
        }

        private async void PlotManager_OnShowStep(IStep<PlotStepType> scene)
        {
            if (scenes.Contains(scene.StepType))
            {
                await saveManager.SaveAsync();
            }
        }
    }
}
