using Assets.Game.Camp;
using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.Intro.Step;
using Assets.Game.Plot.Core;
using Assets.Game.Plot.UI;
using Assets.GameEngine;
using Assets.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Game.Plot.Steps
{
    //13
    class PStep13GameTitle : PlotStep, ILeaveGameComponentDI
    {
        public override event Action OnFinishStep;
        public override event Action<INarrativeStep<PlotStepType>> OnLaunchStep;

        private readonly ShowUIElementsModel showUIElementsModel;
        private readonly PopupManager popupManager;
        private GameTitleConfig config;

        private BlackPanelPopup blackPanel;
        //private CompositeDisposable disposables = new CompositeDisposable();

        CancellationTokenSource cancelTokenSource;
        
        public PStep13GameTitle(PlotConfig plotConfig, ShowUIElementsModel showUIElementsModel, PopupManager popupManager, GameSystemDIController zenjectGameSystem)
        {
            this.stepType = PlotStepType.GameTitle;
            this.showUIElementsModel = showUIElementsModel;
            this.popupManager = popupManager;
            this.config = plotConfig.gameTitlePlot;
            zenjectGameSystem.AddComponent(this);
        }


        public override void Begin()
        {
            OnLaunchStep?.Invoke(this);
            showUIElementsModel.Init(PopupType.PlotGameTitle, config.startDelayTime, config.appearTime, config.stayTime, config.fadeTime);
            showUIElementsModel.Begin();
            showUIElementsModel.OnFinish += Finish;

            var blackShowTime = config.startDelayTime + config.appearTime + config.stayTime - 3;
            cancelTokenSource = new CancellationTokenSource();
            ShowBlackout(blackShowTime, cancelTokenSource.Token);
            //Observable.Timer(TimeSpan.FromSeconds(blackShowTime)).Subscribe(_ => ShowBlackout()).AddTo(disposables);

        }

        private async void ShowBlackout(float time, CancellationToken token)
        {
            await Task.Delay((int)time * 1000);
            if(token.IsCancellationRequested)
            {
                return;
            }
            blackPanel = popupManager.ShowPopup(PopupType.BlackPanelPopup) as BlackPanelPopup;
            blackPanel.Show(0.002f, 0, config.blackoutTime);
        }
        //private void ShowBlackout()
        //{
            
        //}


        public override void Finish()
        {
            cancelTokenSource.Cancel();
            //disposables.Clear();
            blackPanel.Stop();
            OnFinishStep?.Invoke();

        }

        void ILeaveGameComponentDI.LeaveGame()
        {
            //disposables.Clear();
            if(cancelTokenSource!=null)
                cancelTokenSource.Cancel();
            showUIElementsModel.OnFinish -= Finish;
        }
    }
}
