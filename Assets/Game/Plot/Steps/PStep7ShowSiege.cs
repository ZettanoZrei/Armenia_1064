﻿using Assets.Game.Core;
using Assets.Game.Plot.Core;
using Assets.Game.Plot.Scripts;
using Assets.Game.Plot.UI;
using Assets.GameEngine;
using Assets.Modules;
using ExtraInjection;
using GameSystems.Modules;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Game.Plot.Steps
{
    //7
    class PStep7ShowSiege : PlotStep, IExtraInject, IGameLeave
    {
        [ExtraInject] private PopupManager popupManager;
        private readonly PlotStoryModel storyModel;
        private PlotStoryPopup popup;
        private PlotStoryPresenter plotStoryPresenter;
        private PlotCamerasViewConponent cameraConponent;

        public override event Action OnFinishStep;
        public override event Action<IStep<PlotStepType>> OnLaunchStep;


        public PStep7ShowSiege(PlotStoryModel storyModel)
        {
            this.storyModel = storyModel;
            this.stepType = PlotStepType.Siege;
        }

        void IGameLeave.LeaveGame()
        {
            if (plotStoryPresenter != null)
                plotStoryPresenter.OnFinish -= Finish;
        }

        public override void Begin()
        {
            popup = popupManager.ShowPopup(PopupType.PlotStoryPopup) as PlotStoryPopup;
            plotStoryPresenter = new PlotStoryPresenter(popup, storyModel);
            plotStoryPresenter.OnFinish += Finish;
            plotStoryPresenter.Begin();
            cameraConponent = MonoBehaviour.Instantiate(storyModel.PicturePrefab).GetComponent<PlotCamerasViewConponent>();

            cameraConponent.StartCoroutine(Timer(cameraConponent));
            OnLaunchStep?.Invoke(this);
        }


        private IEnumerator Timer(PlotCamerasViewConponent cameraConponent)
        {
            yield return new WaitForEndOfFrame();
            cameraConponent.cameraTwo.Priority = cameraConponent.cameraOne.Priority + 1;
        }

        public override void Finish()
        {
            plotStoryPresenter.OnFinish -= Finish;
            popupManager.ClosePopup(PopupType.PlotStoryPopup);
            cameraConponent.gameObject.SetActive(false);
            OnFinishStep?.Invoke();
        }
    }
}
