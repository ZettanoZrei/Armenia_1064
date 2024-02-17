using Assets.Game.Plot.UI;
using ExtraInjection;
using System;
using UniRx;
using Zenject;

namespace Assets.Game.Intro.Step
{
    class ShowUIElementsModel
    {
        [Inject] private PopupManager popupManager;
        private ShowUiElements popup;
        private PopupType popupType;
        private float startDelayTime;
        private float appearTime;
        private float stayTime;
        private float fadeTime;
        private CompositeDisposable disposables = new CompositeDisposable();

        public event Action OnFinish;

        public void Init(PopupType popupType, float startDelayTime, float appearTime, float stayTime, float fadeTime)
        {
            this.popupType = popupType;
            this.startDelayTime = startDelayTime;
            this.stayTime = stayTime;
            this.fadeTime = fadeTime;
            this.appearTime = appearTime;
        }
        public void Begin()
        {
            Observable.Timer(TimeSpan.FromSeconds(startDelayTime), Scheduler.MainThreadIgnoreTimeScale).Subscribe(_ =>
            {
                ShowGameTitle();
            })
            .AddTo(disposables);
        }

        private void ShowGameTitle()
        {
            popup = popupManager.ShowPopup(popupType, false) as ShowUiElements;
            popup.OnAppeared += PopupStayThenFade;
            popup.OnFaded += Finsih;
            popup.MakeTransparent();
            popup.Appear(appearTime);

        }
        private void PopupStayThenFade()
        {
            Observable.Timer(TimeSpan.FromSeconds(stayTime), Scheduler.MainThreadIgnoreTimeScale).Subscribe(_ =>
            {
                popup.FadeOut(fadeTime);
            }).AddTo(disposables);
        }
        public void Finsih()
        {
            if (popup != null)
            {
                popup.OnAppeared -= PopupStayThenFade;
                popup.OnFaded -= Finsih;
                popupManager.ClosePopup(popupType);
            }
            disposables.Clear();
            OnFinish?.Invoke();
        }
    }
}
