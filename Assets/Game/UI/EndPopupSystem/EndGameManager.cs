using Assets.Game.HappeningSystem;
using ExtraInjection;
using GameSystems.Modules;
using Model.Entities.Happenings;
using System;
using UniRx;
using Zenject;

namespace Assets.Game.UI.EndPopupSystem
{
    class EndGameManager : 
        IInitializable, 
        ILateDisposable, 
        IExtraInject
    {
        [ExtraInject] private PopupManager popupManager;
        private readonly MySceneManager sceneManager;
        private EndGamePopup popup;

        public EndGameManager(MySceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        void IInitializable.Initialize()
        {
            //happeningManager.OnFinishHappening += CheckLastHappening;
        }

        void ILateDisposable.LateDispose()
        {
            //happeningManager.OnFinishHappening -= CheckLastHappening;
        }

        private void CheckLastHappening(Happening happening)
        {
            if (happening.Title == "Шаваршан_Диалог")
            {
                Observable.Timer(TimeSpan.FromSeconds(1), Scheduler.MainThreadIgnoreTimeScale).Subscribe(_ =>
                {
                    popup = popupManager.ShowPopup(PopupType.EndGamePopup) as EndGamePopup;
                    popup.OnClick += EndGame;
                });
            }
        }

        private void EndGame()
        {
            popup.OnClick -= EndGame;
            popupManager.ClosePopup(PopupType.EndGamePopup);
            sceneManager.LoadMainMenuScene();
        }
    }
}
