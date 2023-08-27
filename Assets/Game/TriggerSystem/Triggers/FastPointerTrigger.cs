using Model.Entities.Answers;
using ModestTree.Util;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class FastPointerTrigger : BaseFiniteTrigger
    {
        public event Action<SingleQuestConsequences> OnFastPointerTrigger;
        public UnityEvent<FastPointer> onFastPointerTrigger;

        [SerializeField] private List<FastPointer> fastPointer;

        public void Init(List<FastPointer> fastPointer)
        {
            this.fastPointer = fastPointer;
        }

        protected override void EnterCaravanAction()
        {
            IsDone = true;
            foreach (FastPointer pointer in fastPointer)
            {
                if (!pointer.IsFilled)
                    continue;
                onFastPointerTrigger?.Invoke(pointer);
                OnFastPointerTrigger?.Invoke(new SingleQuestConsequences { Quest = pointer.quest, Happening = pointer.happening });
            };
        }
    }

    [Serializable]
    public class FastPointer
    {
        public string quest;
        public string happening;
        public bool IsFilled => !string.IsNullOrEmpty(quest);
    }
}
