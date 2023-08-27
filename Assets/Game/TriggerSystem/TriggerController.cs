using Assets.Game.DialogBackTriggers;
using Assets.Game.HappeningSystem;
using Entities;
using GameSystems;
using Model.Entities.Persons;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class TriggerController : MonoBehaviour,
        IGameReadyElement, IGameFinishElement, IGameInitElement
    {
        private QuestManager questManager;
        private HappeningManager happeningManager;
        private IEnumerable<ActivatorStaticTrigger> staticRoadTriggers;
        private IEnumerable<LaunchStaticTrigger> beginHappeningTriggers;
        private IEnumerable<FastPointerTrigger> fastPointerTriggers;
        private IEnumerable<CampQuestTriggerModel> campQuestTriggerModes;
        private IEnumerable<DialogBackTrigger> dialogBackTriggers; 
        private IEnumerable<CampIcon> campIcons;
        private DialogBackgroundKeeper backgroundManager;


        [Inject]
        public void Construct(HappeningManager happeningManager, QuestManager questManager, DialogBackgroundKeeper backgroundManager)
        {
            this.questManager = questManager;
            this.backgroundManager = backgroundManager;
            this.happeningManager = happeningManager;
        }

        void IGameInitElement.InitGame(IGameSystem gameSystem)
        {
            //find another way to get links
            beginHappeningTriggers = FindObjectsOfType<LaunchStaticTrigger>();
            staticRoadTriggers = FindObjectsOfType<ActivatorStaticTrigger>();
            fastPointerTriggers = FindObjectsOfType<FastPointerTrigger>(); 
            campQuestTriggerModes = FindObjectsOfType<CampQuestTriggerModel>();
            dialogBackTriggers = FindObjectsOfType<DialogBackTrigger>();
        }
        void IGameFinishElement.FinishGame()
        {
            Unsubscribe();
        }

        void IGameReadyElement.ReadyGame()
        {
            campIcons = FindObjectsOfType<CampIcon>();
            Subscribe();
        }

        private void ActivateHappening(string quest)
        {
            happeningManager.PutHappeningToQueue(quest);
        }

        private void CancelHappening(string quest)
        {
            happeningManager.CancelHappening(quest);
        }

        private void LaunchHappenFromQueue()
        {
            happeningManager.LaunchHappenFromQueue();
        }

        //for camp quest
        private void LaunchHappenOutOfQueue(string quest)
        {
            happeningManager.LaunchHappenWithoutQueue(quest);
        }

        private void Subscribe()
        {
            foreach (var trigger in campIcons)
                trigger.OnIconClick += LaunchHappenOutOfQueue;

            foreach (var trigger in staticRoadTriggers)
                trigger.OnActivateQuest += ActivateHappening;

            foreach (var trigger in beginHappeningTriggers)
                trigger.OnLaunchHappening += LaunchHappenFromQueue;

            foreach (var trigger in fastPointerTriggers)
                trigger.OnFastPointerTrigger += questManager.WriteDownQuestConsequences;

            foreach (var trigger in campQuestTriggerModes)
                trigger.OnLoadStateQuestBack += questManager.WriteDownQuestConsequences;

            foreach (var trigger in dialogBackTriggers)
                trigger.OnChangeDialogBack += backgroundManager.SetDialogBackground;

        }

        private void Unsubscribe()
        {
            foreach (var trigger in campIcons)
                trigger.OnIconClick -= LaunchHappenOutOfQueue;

            foreach (var trigger in staticRoadTriggers)
                trigger.OnActivateQuest -= ActivateHappening;

            foreach (var trigger in beginHappeningTriggers)
                trigger.OnLaunchHappening -= LaunchHappenFromQueue;

            foreach (var trigger in fastPointerTriggers)
                trigger.OnFastPointerTrigger -= questManager.WriteDownQuestConsequences;

            foreach (var trigger in campQuestTriggerModes)
                trigger.OnLoadStateQuestBack -= questManager.WriteDownQuestConsequences;

            foreach (var trigger in dialogBackTriggers)
                trigger.OnChangeDialogBack -= backgroundManager.SetDialogBackground;
        }
    }
}
