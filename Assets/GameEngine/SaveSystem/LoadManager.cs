using Assets.Game;
using Assets.Game.HappeningSystem.Persons;
using Assets.Game.HappeningSystem;
using Assets.Save;
using Model.Types;
using Assets.Game.Parameters;
using Model.Entities.Answers;
using Assets.Game.Camp;
using Assets.Game.Timer;
using Assets.Game.Plot.Core;
using Assets.Game.Parameters.EndedParamSystem;
using Entities;
using Zenject;
using Assets.Game.Configurations;
using Assets.Game.Tutorial.Core;
using System.Linq;
using ExtraInjection;

public class LoadManager : IExtraInject
{
    private readonly Repository repository;
    private readonly QuestManager questManager;
    private readonly ConfigurationRuntime configurationRuntime;
    private readonly DialogBackgroundKeeper backgroundKeeper;
    private readonly RelationManager relationManager;
    private readonly ParametersManager parametersManager;
    private readonly TravelSceneNavigator travelSceneNavigator;
    private readonly BSRepositoryCaravan repositoryCaravan;
    private readonly BSRepositoryTrigger repositoryTriggers;
    private readonly BSRepositoryCampQuestTrigger repositoryCampQuests;
    private readonly HappeningReplaceManager replaceManager;
    private readonly CampIncomingData campIncomingData;
    private readonly TimeMechanics timeManager;
    private readonly DialogPersonPackCatalog personPacks;
    private readonly PlotManager plotManager;
    private readonly TutorialManager tutorialManager;
    private readonly HappeningManager happeningManager;
    [ExtraInject] private EndedParamMechanics endedParamMechanics;

    public LoadManager(Repository repository, QuestManager questManager, ConfigurationRuntime configurationRuntime, DialogBackgroundKeeper backgroundManager,
        RelationManager relationManager, ParametersManager parametersManager, TravelSceneNavigator travelSceneNavigator, BSRepositoryCaravan repositoryCaravan,
        BSRepositoryTrigger repositoryTriggers, BSRepositoryCampQuestTrigger repositoryCampQuests, HappeningManager happeningManager,
        HappeningReplaceManager replaceManager, CampIncomingData campIncomingData, TimeMechanics timeManager, 
        DialogPersonPackCatalog personPacks, PlotManager plotManager, TutorialManager tutorialManager)
    {
        this.repository = repository;
        this.questManager = questManager;
        this.configurationRuntime = configurationRuntime;
        this.backgroundKeeper = backgroundManager;
        this.relationManager = relationManager;
        this.parametersManager = parametersManager;
        this.travelSceneNavigator = travelSceneNavigator;
        this.repositoryCaravan = repositoryCaravan;
        this.repositoryTriggers = repositoryTriggers;
        this.repositoryCampQuests = repositoryCampQuests;
        this.replaceManager = replaceManager;
        this.campIncomingData = campIncomingData;
        this.timeManager = timeManager;
        this.endedParamMechanics = endedParamMechanics;
        this.personPacks = personPacks;
        this.plotManager = plotManager;
        this.tutorialManager = tutorialManager;
        this.happeningManager = happeningManager;
    }

    public void LoadData()
    {
        repository.LoadSaveData();
        LoadCaravanPosition();
        LoadCampBackground();
        LoadDialogBackground();
        LoadTriggers();
        LoadCampQuestTrigger();
        LoadQuests();
        LoadRelations();
        LoadParameters();
        LoadTravelScenePointer();
        LoadHappenReplacements();
        LoadTime();
        LoadPlotStep();
        LoadTutorialStep();
        LoadParamsTimers();
        LoadActivedHappening();
        SavePersonPackIndex();
    }



    public Scene LoadScene()
    {
        return repository.LoadScene();
    }

    private void SavePersonPackIndex()
    {
        var value = repository.LoadPersonPackIndex();
        foreach (var person in personPacks)
        {
            person.complectIndex = value;
            person.portraitIndex = value;
        }
    }
    private void LoadActivedHappening()
    {
        foreach (var happenName in repository.LoadActivedHappening())
        {
            happeningManager.PutHappenigToQueueForLoader(happenName);
        }
    }
    private void LoadParamsTimers()
    {
        foreach (var timer in repository.LoadParamsTimers())
        {
            endedParamMechanics.Timers[timer.Key].State = timer.Value.Item1;
            endedParamMechanics.Timers[timer.Key].SetCurrentTime(timer.Value.Item2);
            endedParamMechanics.Timers[timer.Key].SetDuration(timer.Value.Item3);
        }
    }

    private void LoadPlotStep()
    {
        var data = repository.LoadPlotStep();
        configurationRuntime.PlotConfig.startStep = (PlotStepType)data.CurrentStep;
        plotManager.IsComplete = data.IsComplete;
        plotManager.LastShownStep = (PlotStepType)data.LastShowStep;
    }

    private void LoadTutorialStep()
    {
        var data = repository.LoadTutorialStep();
        configurationRuntime.TutorialConfig.startStep = (TutorialStepType)data.CurrentStep; 
        tutorialManager.IsComplete = data.IsComplete;
        tutorialManager.LastShownStep = (TutorialStepType)data.LastShowStep;
    }
    private void LoadParameters()
    {
        parametersManager.SetParameter(repository.LoadParameter(ParameterType.Spirit), ParameterType.Spirit);
        parametersManager.SetParameter(repository.LoadParameter(ParameterType.Food), ParameterType.Food);
        parametersManager.SetParameter(repository.LoadParameter(ParameterType.People), ParameterType.People);
        parametersManager.SetParameter(repository.LoadParameter(ParameterType.Stamina), ParameterType.Stamina);
    }
    private void LoadRelations()
    {
        foreach (var relation in repository.LoadRelations())
        {
            relationManager.InitPersonRelation(relation.Key, relation.Value);
        }
    }
    private void LoadQuests()
    {
        foreach (var quest in repository.LoadQuests())
        {
            questManager.AddQuest(quest.Value);
        }
    }

    private void LoadCampQuestTrigger()
    {
        foreach(var sceneData in repository.LoadCampQuestTrigger())
        {
            repositoryCampQuests.Add(sceneData.Key, sceneData.Value);
        }
    }
    private void LoadTriggers()
    {
        foreach(var sceneData in repository.LoadTrigger())
        {
            repositoryTriggers.Add(sceneData.Key, sceneData.Value);
        }
    }
    private void LoadDialogBackground()
    {
        var data = repository.LoadDialogBack();
        backgroundKeeper.ChangeState((DialogBackgroundKeeper.BackgroundState)data.state);
        backgroundKeeper.SetDialogBackground(data);
    }
    private void LoadCampBackground()
    {
        var data = repository.LoadCampData();
        campIncomingData.CampImagePrefab = data.Item1;
        Logger.WriteLog($"LoadCampBackground: {campIncomingData.CampImagePrefab}");
        campIncomingData.CampQuests = data.Item3.ToList();
        campIncomingData.SetDialogAvailable(data.Item2);
    }
    private void LoadCaravanPosition()
    {
        var data = repository.LoadCaravanPosition();
        foreach(var sceneData in data)
        {
            repositoryCaravan.Add(sceneData.Key, sceneData.Value);
        }
    }

    private void LoadTravelScenePointer()
    {
        travelSceneNavigator.SetNextScene(repository.LoadTravelScene());
    }

    private void LoadHappenReplacements()
    {
        foreach (var item in repository.LoadReplacement())
        {
            replaceManager.AddHappeningRaplacement(new SingleHappeningConsequences { OldHappening = item.Key, NewHappening = item.Value });
        }
    }

    private void LoadTime()
    {
        timeManager.SetDays(repository.LoadTime());
    }
}
