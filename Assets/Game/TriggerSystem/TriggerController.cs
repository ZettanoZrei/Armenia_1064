using Assets.Game.DialogBackTriggers;
using Assets.Game.Stoppage;
using Assets.Modules;
using GameSystems.Modules;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class TriggerController : IInitializable,
        IGameInitElement,
        IGameReadyElement, 
        IGameFinishElement
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
        private readonly SignalBus signalBus;
        private readonly FiniteTriggerCatalog finiteTriggerCatalog;

        public TriggerController(HappeningManager happeningManager, QuestManager questManager, DialogBackgroundKeeper backgroundManager, SignalBus signalBus,
            FiniteTriggerCatalog finiteTriggerCatalog)
        {
            this.questManager = questManager;
            this.backgroundManager = backgroundManager;
            this.signalBus = signalBus;
            this.finiteTriggerCatalog = finiteTriggerCatalog;
            this.happeningManager = happeningManager;             
        }
        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }

        void IGameInitElement.InitGame()
        {
            staticRoadTriggers = finiteTriggerCatalog.GetElements<ActivatorStaticTrigger>();
            beginHappeningTriggers = finiteTriggerCatalog.GetElements<LaunchStaticTrigger>();
            fastPointerTriggers = finiteTriggerCatalog.GetElements<FastPointerTrigger>();
            campQuestTriggerModes = finiteTriggerCatalog.GetElements<CampQuestTriggerModel>();
            dialogBackTriggers = finiteTriggerCatalog.GetElements<DialogBackTrigger>();
            //stoppageTriggers = finiteTriggerCatalog.GetElements<StoppageTrigger>(); //TODO: оно здесь должно быть?
        }

        void IGameReadyElement.ReadyGame()
        {
            //campIcons = MonoBehaviour.FindObjectsOfType<CampIcon>();
            Subscribe();
        }
        void IGameFinishElement.FinishGame()
        {
            Unsubscribe();
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
