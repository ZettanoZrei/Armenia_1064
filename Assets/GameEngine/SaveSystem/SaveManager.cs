using Assets.Game;
using Assets.Game.Camp;
using Assets.Game.Configurations;
using Assets.Game.HappeningSystem;
using Assets.Game.HappeningSystem.Persons;
using Assets.Game.Parameters;
using Assets.Game.Parameters.EndedParamSystem;
using Assets.Game.Plot.Core;
using Assets.Game.Timer;
using Assets.Game.Tutorial.Core;
using Assets.Save;
using Entities;
using ExtraInjection;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Systems.SaveSystem
{
    public class SaveManager : IExtraInject
    {
        private readonly DialogBackgroundKeeper backgroundManager;
        private readonly MySceneManager sceneManager;
        private readonly Repository repository;
        private readonly QuestManager questManager;
        private readonly RelationManager relationManager;
        private readonly ParametersManager parametersManager;
        private readonly TravelSceneNavigator travelSceneNavigator;
        private readonly BSRepositoryCaravan repositoryCaravan;
        private readonly BSRepositoryTrigger repositoryTriggers;
        private readonly BSRepositoryCampQuestTrigger repositoryCampQuests;
        private readonly HappeningReplaceManager replaceManager;
        private readonly CampIncomingData campIncomingData;
        private readonly TimeMechanics timeManager;
        private readonly TutorialManager tutorialManager;
        private readonly PlotManager plotManager;
        private readonly HappeningManager happeningManager;
        private readonly DialogPersonPackCatalog personPacks;
        [ExtraInject] private EndedParamMechanics endedParamMechanics;

        public SaveManager(Repository repository, DialogBackgroundKeeper backgroundManager, MySceneManager sceneManager, QuestManager questManager,
                RelationManager relationManager, ParametersManager parametersManager, TravelSceneNavigator travelSceneNavigator, PlotManager plotManager,
                BSRepositoryCaravan repositoryCaravan, BSRepositoryTrigger repositoryTriggers, BSRepositoryCampQuestTrigger repositoryCampQuests,
                HappeningReplaceManager replaceManager, CampIncomingData campIncomingData, TimeMechanics timeManager,
                TutorialManager tutorialManager, HappeningManager happeningManager,
                DialogPersonPackCatalog personPacks)
        {
            this.backgroundManager = backgroundManager;
            this.sceneManager = sceneManager;
            this.repository = repository;
            this.questManager = questManager;
            this.relationManager = relationManager;
            this.parametersManager = parametersManager;
            this.travelSceneNavigator = travelSceneNavigator;
            this.repositoryCaravan = repositoryCaravan;
            this.repositoryTriggers = repositoryTriggers;
            this.repositoryCampQuests = repositoryCampQuests;
            this.replaceManager = replaceManager;
            this.campIncomingData = campIncomingData;
            this.timeManager = timeManager;
            this.tutorialManager = tutorialManager;
            this.plotManager = plotManager;
            this.happeningManager = happeningManager;
            this.personPacks = personPacks;
        }

        public async Task SaveAsync()
        {
            repository.InitNewSaveData();
            FillSaveData();

            await repository.Save();
        }

        public void FillSaveData()
        {
            //repository.ClearSaveModel();
            SaveBackground();
            SaveCaravanPosition();
            SaveTriggers();
            SaveCampQuestTrigger();
            SaveQuests();
            SaveRelations();
            SaveParameters();
            SaveScene();
            SaveTravelScene();
            SaveReplacements();
            SaveTime();
            SavePlotStep();
            SaveParamsTimers();
            SaveActivedHappening();
            SaveTutorialStep();
            SavePersonPackIndex();
        }

        private void SavePersonPackIndex()
        {
            repository.SavePersonPackIndex(personPacks.First().complectIndex);
        }
        private void SaveActivedHappening()
        {
            foreach (var happen in happeningManager.EnumerableHappenings())
            {
                repository.SaveActivedHappenig(happen.Title);
            }
        }
        private void SavePlotStep()
        {
            repository.SavePlotState(plotManager.CurrentStepIndex, plotManager.IsComplete, (int)plotManager.LastShownStep);
        }

        private void SaveTutorialStep()
        {
            repository.SaveTutorialStep(tutorialManager.CurrentStepIndex, tutorialManager.IsComplete, (int)tutorialManager.LastShownStep);
        }

        private void SaveParamsTimers()
        {
            foreach (var timer in endedParamMechanics.Timers)
            {
                repository.SaveParamsTimers(timer.Key, timer.Value.State, timer.Value.CurrentTime, timer.Value.Duration);
            }
        }

        private void SaveParameters()
        {
            repository.SaveParameter(ParameterType.People, parametersManager.People.Value);
            repository.SaveParameter(ParameterType.Food, parametersManager.Food.Value);
            repository.SaveParameter(ParameterType.Spirit, parametersManager.Spirit.Value);
            repository.SaveParameter(ParameterType.Stamina, parametersManager.Stamina.Value);
        }
        private void SaveRelations()
        {
            foreach (var item in relationManager)
            {
                repository.SaveRelations(item.Name, item.Value.Value);
            }
        }
        private void SaveQuests()
        {
            foreach (var quest in questManager)
            {
                repository.SaveQuest(quest);
            }
        }

        private void SaveCampQuestTrigger()
        {
            foreach (var trigger in repositoryCampQuests.Triggers)
            {
                repository.SaveCampQuestTrigger(trigger.Key, trigger.Value);
            }
        }
        private void SaveTriggers()
        {
            foreach (var trigger in repositoryTriggers.Triggers)
            {
                repository.SaveTrigges(trigger.Key, trigger.Value);
            }
        }
        private void SaveCaravanPosition()
        {
            foreach (var sceneData in repositoryCaravan.PositionData)
            {
                repository.SaveCaravanPosition(sceneData.Key, sceneData.Value);
            }
        }
        private void SaveBackground()
        {
            Logger.WriteLog($"SaveBackground: {campIncomingData.CampImagePrefab}");
            repository.SaveCampData(campIncomingData.CampImagePrefab, campIncomingData.DialogAvailable, campIncomingData.CampQuests);
            var pack = backgroundManager.BackgroundPack;
            repository.SaveDialogBack(pack.dialogFrontTravel, pack.dialogBackTravel, pack.dialogFrontCamp, pack.dialogBackCamp, (int)backgroundManager.State);
        }

        private void SaveScene()
        {
            if (sceneManager.IsSystemSceme(sceneManager.CurrentScene))
                return;
            repository.SaveScene(sceneManager.CurrentScene);
        }
        private void SaveTravelScene()
        {
            repository.SaveTravelScene(travelSceneNavigator.ScenePointer);
        }

        private void SaveReplacements()
        {
            foreach (var item in replaceManager)
            {
                repository.SaveReplacement(item.Key, item.Value);
            }
        }

        private void SaveTime()
        {
            repository.SaveTime(timeManager.Days + timeManager.Time);
        }

    }
}
