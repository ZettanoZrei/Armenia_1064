using System;
using UnityEngine;

namespace Assets.Game.HappeningSystem
{
    //for static event on road
    class ActivatorStaticTrigger : BaseFiniteTrigger
    {
        [SerializeField]
        private string quest;

        public event Action<string> OnActivateQuest;

        protected override void EnterCaravanAction()
        {
            IsDone = true;
            OnActivateQuest?.Invoke(quest);
        }
    }
}
