using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.Plot.Core;
using Assets.Game.Plot.Scripts;
using Assets.Game.Plot.UI;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Plot.Steps
{
    //1
    class PStep1ShowMapPopup : PlotStep
    {
        private readonly PopupManager popupManager;
        private readonly MySceneManager sceneManager;
        private readonly PlotStoryModel storyModel;
        private readonly PlotConfig plotConfig;
        private PlotStoryPresenter plotStoryPresenter;
        private PlotStoryPopup popup;

        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;

        public PStep1ShowMapPopup(PopupManager popupManager, MySceneManager sceneManager, PlotStoryModel storyModel, ConfigurationRuntime config)
        {
            this.popupManager = popupManager;
            this.sceneManager = sceneManager;
            this.storyModel = storyModel;
            this.plotConfig = config.PlotConfig;
            this.stepType = PlotStepType.ShowMap;
        }

        public override void Begin()
        {
            popup = popupManager.ShowPopup(PopupType.PlotStoryPopup) as PlotStoryPopup;
            plotStoryPresenter = new PlotStoryPresenter(popup, storyModel);
            plotStoryPresenter.OnFinish += Finish;
            plotStoryPresenter.Begin();
            var mapComponent = MonoBehaviour.Instantiate(storyModel.PicturePrefab).GetComponent<PlotMapViewComponent>();

            mapComponent.StartCoroutine(Timer(mapComponent.Map));
            OnLaunchStep?.Invoke(this);
        }

        private IEnumerator Timer(GameObject map)
        {
            while (map.transform.localScale.x < plotConfig.mapPlot.aimScale)
            {
                var coef = Mathf.SmoothStep(plotConfig.mapPlot.stepMax, plotConfig.mapPlot.stepMin, map.transform.localScale.x / plotConfig.mapPlot.aimScale);
                map.transform.localScale = new Vector3(map.transform.localScale.x + coef, map.transform.localScale.y + coef,
                    map.transform.localScale.z);
                yield return new WaitForSeconds(plotConfig.mapPlot.timeInterval);
            }
        }
        private void CheckPlotScene(Scene scene)
        {
            if (scene == Scene.PlotScene)
            {
                DoBegin();
            }
        }

        private void DoBegin()
        {
            sceneManager.OnChangeScene_Post -= CheckPlotScene;

        }

        public override void Finish()
        {
            plotStoryPresenter.OnFinish -= Finish;
            popupManager.ClosePopup(PopupType.PlotStoryPopup);
            OnFinishStep?.Invoke();
        }
    }
}
