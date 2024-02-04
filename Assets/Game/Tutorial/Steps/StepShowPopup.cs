using Assets.Game.Tutorial.Core;
using Assets.Game.Tutorial.UI;
using Assets.GameEngine;
using Assets.Modules;
using ExtraInjection;
using GameSystems.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Game.Tutorial.Steps
{
    internal abstract class StepShowPopup : TutorialStep, IInitializable, ISceneFinish, IExtraInject
    {
        [ExtraInject] protected PopupManager popupManager;
        public override event Action OnFinishStep;
        protected readonly PopupType popupType;
        private readonly SignalBus signalBus;
        protected TutorialPopup popup;

        public StepShowPopup(PopupType popupType, SignalBus signalBus)
        {
            this.popupType = popupType;
            this.signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        public override void Finish()
        {
            popup.OnFinish -= Finish;
            popupManager.ClosePopup(popupType);
            OnFinishStep?.Invoke();
        }
        public abstract void FinishScene();       
    }
}
