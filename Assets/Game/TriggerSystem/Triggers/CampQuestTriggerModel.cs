using Model.Entities.Answers;
using ModestTree.Util;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    public class CampQuestTriggerModel : BaseFiniteTrigger, ISerializationCallbackReceiver
    {
        public event Action<SingleQuestConsequences> OnLoadStateQuestBack;

        [SerializeField] private FastPointerTrigger fastPointerTrigger;
        [SerializeField] private bool IsLoadStateBack = true;
        [Inject] private QuestManager questManager;

        private List<FastPointer> campQuests = new List<FastPointer>
        {
            new FastPointer{ quest= "Василий" },
            new FastPointer{ quest= "Наира" },
        };

        private List<SingleQuestConsequences> savesState = new List<SingleQuestConsequences>();

        public List<SingleQuestConsequences> SavesState { get => savesState; set => savesState = value; }

        //ui fast pointer
        public void SaveQuestState(FastPointer fastPointer)
        {
            var pointer = questManager.GetHappeningName(fastPointer.quest);
            savesState.Add(new SingleQuestConsequences { Quest = fastPointer.quest, Happening = pointer });
        }

        private void LoadStateBack()
        {
            if (!IsLoadStateBack)
                return;
            foreach (var state in savesState)
                OnLoadStateQuestBack?.Invoke(state);
        }

        
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            fastPointerTrigger.Init(campQuests);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        protected override void ExitCaravanAction()
        {
            IsDone = true;
            LoadStateBack();
        }
    }
}
