using Assets.Game.Camp;
using Assets.Game.DialogBackTriggers;
using Assets.Game.Stoppage;
using Assets.Save;
using GameSystems;
using Model.Entities.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class TriggerRepositoryBSController : MonoBehaviour,
        IGameReadyElement, IGameFinishElement, IGameInitElement, IGameChangeSceneElement, IGameStartElement
    {
        private IEnumerable<ActivatorStaticTrigger> staticRoadTriggers;
        private IEnumerable<LaunchStaticTrigger> beginHappeningTriggers;
        private IEnumerable<FastPointerTrigger> fastPointerTriggers;
        private IEnumerable<CampQuestTriggerModel> campQuestTriggerModes;
        private IEnumerable<DialogBackTrigger> dialogBackTriggers;
        private IEnumerable<StoppageTrigger> stoppageTriggers;

        private BSRepositoryTrigger repositoryBS;
        private BSRepositoryCampQuestTrigger bSRepositoryCampQuestTrigger;
        private FiniteTriggerCatalog finiteTriggerCatalog;
        private MySceneManager sceneManager;

        [Inject]
        public void Construct(BSRepositoryTrigger repositoryBS, BSRepositoryCampQuestTrigger bSRepositoryCampQuestTrigger, FiniteTriggerCatalog finiteTriggerCatalog,
            MySceneManager sceneManager)
        {
            this.repositoryBS = repositoryBS;
            this.bSRepositoryCampQuestTrigger = bSRepositoryCampQuestTrigger;
            this.finiteTriggerCatalog = finiteTriggerCatalog;
            this.sceneManager = sceneManager;
        }
        void IGameInitElement.InitGame(IGameSystem _)
        {
            staticRoadTriggers = finiteTriggerCatalog.GetElements<ActivatorStaticTrigger>(); 
            beginHappeningTriggers = finiteTriggerCatalog.GetElements<LaunchStaticTrigger>();
            fastPointerTriggers = finiteTriggerCatalog.GetElements<FastPointerTrigger>();
            campQuestTriggerModes = finiteTriggerCatalog.GetElements<CampQuestTriggerModel>();
            dialogBackTriggers = finiteTriggerCatalog.GetElements<DialogBackTrigger>();
            stoppageTriggers = finiteTriggerCatalog.GetElements<StoppageTrigger>();
        }

        void IGameReadyElement.ReadyGame()
        {           
            Subscribe();
        }

        void IGameStartElement.StartGame()
        {
            LoadTriggersBetweenScenes();
        }
        void IGameChangeSceneElement.ChangeScene()
        {
            Unsubscribe();
        }
        void IGameFinishElement.FinishGame()
        {
            Unsubscribe();
        }
        private void Subscribe()
        {
            foreach (var trigger in staticRoadTriggers)
                trigger.OnActivateQuest += Save;

            foreach (var trigger in beginHappeningTriggers)
                trigger.OnLaunchHappening += SaveTriggersBetweenScenes;

            foreach (var trigger in fastPointerTriggers)
                trigger.OnFastPointerTrigger += Save;

            foreach (var trigger in campQuestTriggerModes)
                trigger.OnLoadStateQuestBack += Save;

            foreach (var trigger in dialogBackTriggers)
                trigger.OnChangeDialogBack += Save;

            foreach (var trigger in stoppageTriggers)
                trigger.OnCaravanCollision += SaveTriggersBetweenScenes;
        }
        private void Unsubscribe()
        {
            foreach (var trigger in staticRoadTriggers)
                trigger.OnActivateQuest -= Save;

            foreach (var trigger in beginHappeningTriggers)
                trigger.OnLaunchHappening -= SaveTriggersBetweenScenes;

            foreach (var trigger in fastPointerTriggers)
                trigger.OnFastPointerTrigger -= Save;

            foreach (var trigger in campQuestTriggerModes)
                trigger.OnLoadStateQuestBack -= Save;

            foreach (var trigger in dialogBackTriggers)
                trigger.OnChangeDialogBack -= Save;

            foreach (var trigger in stoppageTriggers)
                trigger.OnCaravanCollision -= SaveTriggersBetweenScenes;
        }
        private void LoadTriggersBetweenScenes()
        {
            if (!repositoryBS.TryGetData(sceneManager.CurrentScene, out Dictionary<int, bool> data))
                return;

            ReadTriggersData(staticRoadTriggers);
            ReadTriggersData(beginHappeningTriggers);
            ReadTriggersData(fastPointerTriggers);
            ReadTriggersData(dialogBackTriggers);
            ReadTriggersData(stoppageTriggers);


            if(bSRepositoryCampQuestTrigger.TryGetData(sceneManager.CurrentScene, out Dictionary<int, CampQuestTriggerInfo> info))
            {
                foreach (var trigger in campQuestTriggerModes)
                {
                    if(info.ContainsKey(trigger.Index))
                    {
                        trigger.IsDone = info[trigger.Index].isDone;
                        trigger.SavesState = info[trigger.Index].fastPointers;
                    }
                }
            }


            void ReadTriggersData(IEnumerable<BaseFiniteTrigger> finiteTriggers)
            {
                foreach (var trigger in finiteTriggers)
                    if (data.ContainsKey(trigger.Index))
                        trigger.IsDone = data[trigger.Index];
            }
        }
        private void Save(object  _)
        {
            SaveTriggersBetweenScenes();
        }
        private void SaveTriggersBetweenScenes()
        {
            repositoryBS.Add(sceneManager.CurrentScene, staticRoadTriggers.ToDictionary(x => x.Index, x => x.IsDone));
            repositoryBS.Add(sceneManager.CurrentScene, beginHappeningTriggers.ToDictionary(x => x.Index, x => x.IsDone));
            repositoryBS.Add(sceneManager.CurrentScene, fastPointerTriggers.ToDictionary(x => x.Index, x => x.IsDone));
            repositoryBS.Add(sceneManager.CurrentScene, dialogBackTriggers.ToDictionary(x => x.Index, x => x.IsDone));
            repositoryBS.Add(sceneManager.CurrentScene, stoppageTriggers.ToDictionary(x => x.Index, x => x.IsDone));
            bSRepositoryCampQuestTrigger.Add(sceneManager.CurrentScene, campQuestTriggerModes.ToDictionary(x => x.Index, x => new CampQuestTriggerInfo(x.IsDone, x.SavesState)));
        }       
    }
}
