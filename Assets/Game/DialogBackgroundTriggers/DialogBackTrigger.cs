using Assets.Game.HappeningSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.DialogBackTriggers
{
    internal class DialogBackTrigger : BaseFiniteTrigger
    {
        public string travelFront;
        public string travelBack;
        public string campFront;
        public string campBack;

        public event Action<BackgroundPack> OnChangeDialogBack;

        protected override void EnterCaravanAction()
        {
            Logger.WriteLog($"trigger: {Index}");
            IsDone = true;
            OnChangeDialogBack?.Invoke(new BackgroundPack
            {
                dialogFrontTravel = travelFront,
                dialogBackTravel = travelBack,
                dialogFrontCamp = campFront,
                dialogBackCamp = campBack,
            });
        }
    }
}
