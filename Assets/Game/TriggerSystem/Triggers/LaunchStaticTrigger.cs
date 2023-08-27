using System;
using UnityEngine;

namespace Assets.Game.HappeningSystem
{
    //for launch next happening
    class LaunchStaticTrigger : BaseFiniteTrigger
    {
        public event Action OnLaunchHappening;

        protected override void EnterCaravanAction()
        {
            IsDone = true;
            OnLaunchHappening?.Invoke();
        }
    }
}
