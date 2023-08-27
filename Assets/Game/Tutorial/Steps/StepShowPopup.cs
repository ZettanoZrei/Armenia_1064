using Assets.Game.Tutorial.Core;
using Assets.Game.Tutorial.UI;
using Assets.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Game.Tutorial.Steps
{
    internal abstract class StepShowPopup : TutorialStep, ILeaveGameComponentDI
    {
        public override event Action OnFinishStep;
        protected readonly PopupManager popupManager;
        protected readonly PopupType popupType;
        protected TutorialPopup popup;

        public StepShowPopup(PopupManager popupManager, PopupType popupType, GameSystemDIController zenjectGameSystem)
        {
            this.popupManager = popupManager;
            this.popupType = popupType;
            zenjectGameSystem.AddComponent(this);
        }


        public override void Finish()
        {
            popup.OnFinish -= Finish;
            popupManager.ClosePopup(popupType);
            OnFinishStep?.Invoke();
        }

        public abstract void LeaveGame();
    }


}
