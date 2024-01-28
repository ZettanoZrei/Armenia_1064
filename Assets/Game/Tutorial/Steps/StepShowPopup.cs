using Assets.Game.Tutorial.Core;
using Assets.Game.Tutorial.UI;
using Assets.GameEngine;
using Assets.Modules;
using GameSystems.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Game.Tutorial.Steps
{
    internal abstract class StepShowPopup : TutorialStep, IInitializable, IGameFinishElement
    {
        public override event Action OnFinishStep;
        protected readonly PopupManager popupManager;
        protected readonly PopupType popupType;
        private readonly SignalBus signalBus;
        protected TutorialPopup popup;

        public StepShowPopup(PopupManager popupManager, PopupType popupType, SignalBus signalBus)
        {
            this.popupManager = popupManager;
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
        public abstract void FinishGame();       
    }
}
