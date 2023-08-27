using Assets.Game;
using Assets.Game.HappeningSystem;
using Assets.Game.Plot.Core;
using Assets.Game.Tutorial.Core;
using Assets.Save;
using Assets.Systems.SaveSystem;
using Model.Entities.Happenings;
using Model.Serializers;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zenject;

public class Repository
{
    private readonly SaveHelper<SaveData> saveHelper;
    public SaveData SaveData { get; private set; }

    public Repository(SaveHelper<SaveData> saveHelper)
    {
        this.saveHelper = saveHelper;
        //SaveModel = new SaveData();
    }


    //use before other methods
    public void LoadSaveData()
    {
        SaveData = saveHelper.LoadModel();
    }

    public void InitNewSaveData()
    {
        SaveData = new SaveData();
    }
    public void SaveActivedHappenig(string happening)
    {
        if (!SaveData.ActivedHappenings.Contains(happening))
            SaveData.ActivedHappenings.Add(happening);
    }

    public IEnumerable<string> LoadActivedHappening()
    {
        return SaveData.ActivedHappenings;
    }

    public void SaveParamsTimers(ParameterType parameter, TimerState state, float currentTime, float duration)
    {
        SaveData.ParamTimers[parameter] = (state, currentTime, duration);
    }

    public Dictionary<ParameterType, (TimerState, float currentTime, float duration)> LoadParamsTimers()
    {
        return SaveData.ParamTimers;
    }

    public void SavePlotState(int plotStep, bool isComplete, int lastStep)
    {
        SaveData.PlotStep = new NarrativeData { LastShowStep = lastStep, IsComplete = isComplete, CurrentStep = plotStep };
    }

    public NarrativeData LoadPlotStep()
    {
        return SaveData.PlotStep;
    }

    public void SaveTutorialStep(int tutorialStep, bool isComplete, int lastStep)
    {
        SaveData.TutorialStep = new NarrativeData { LastShowStep = lastStep, IsComplete = isComplete, CurrentStep = tutorialStep };
    }

    public NarrativeData LoadTutorialStep()
    {
        return SaveData.TutorialStep;
    }

    public void SaveQuest(Quest quest)
    {
        SaveData.Quests[quest.Title] = quest;
    }
    public Dictionary<string, Quest> LoadQuests()
    {
        return SaveData.Quests;
    }
    public void SaveCaravanPosition(Scene scene, List<CaravanData> positions)
    {
        SaveData.CaravanPositions[scene] = positions;
    }
    public Dictionary<Scene, List<CaravanData>> LoadCaravanPosition()
    {
        return SaveData.CaravanPositions;
    }
    public void SaveRelations(string person, int relation)
    {
        SaveData.Relations[person] = relation;
    }
    public Dictionary<string, int> LoadRelations()
    {
        return SaveData.Relations;
    }
    public void SaveTrigges(Scene scene, Dictionary<int, bool> triggers)
    {
        SaveData.Triggers[scene] = triggers;
    }
    public Dictionary<Scene, Dictionary<int, bool>> LoadTrigger()
    {
        return SaveData.Triggers;
    }
    public void SaveCampQuestTrigger(Scene scene, Dictionary<int, CampQuestTriggerInfo> info)
    {
        SaveData.CampQuestTrigges[scene] = info;
    }
    public Dictionary<Scene, Dictionary<int, CampQuestTriggerInfo>> LoadCampQuestTrigger()
    {
        return SaveData.CampQuestTrigges;
    }
    public void SaveParameter(ParameterType type, float value)
    {
        SaveData.Parameters[type] = value;
    }
    public float LoadParameter(ParameterType type)
    {
        return SaveData.Parameters[type];
    }
    public void SaveCampData(string name, int dialogs, List<Quest> quests)
    {
        SaveData.CampImagePrefab = name;
        SaveData.CampDialogAvailbale = dialogs;
        SaveData.CampDialogs = quests.Select(x => x.Title).ToList();
    }
    public (string, int, IEnumerable<Quest>) LoadCampData()
    {
        var quests = SaveData.Quests.Select(x => x.Value).Where(x => SaveData.CampDialogs.Contains(x.Title));
        return (SaveData.CampImagePrefab, SaveData.CampDialogAvailbale, quests);
    }
    public void SaveDialogBack(string frontTravel, string backTravel, string frontCamp, string backCamp, int state)
    {
        SaveData.BackgroundPack = new BackgroundPack
        {
            dialogFrontTravel = frontTravel,
            dialogBackTravel = backTravel,
            dialogFrontCamp = frontCamp,
            dialogBackCamp = backCamp,
            state = state
        };
    }
    public BackgroundPack LoadDialogBack()
    {
        return SaveData.BackgroundPack;
    }
    public void SaveScene(Scene scene)
    {
        SaveData.Scene = scene;
    }
    public Scene LoadScene()
    {
        return SaveData.Scene;
    }

    public void SaveTravelScene(Scene scene)
    {
        SaveData.TravelScene = scene;
    }
    public Scene LoadTravelScene()
    {
        return SaveData.TravelScene;
    }

    public void SaveReplacement(string oldHappen, string newHappen)
    {
        SaveData.HappenReplacements[oldHappen] = newHappen;
    }
    public Dictionary<string, string> LoadReplacement()
    {
        return SaveData.HappenReplacements;
    }
    public void SaveTime(float time)
    {
        SaveData.Time = time;
    }

    public float LoadTime()
    {
        return SaveData.Time;
    }

    public void SavePersonPackIndex(int value)
    {
        SaveData.PersonPackIndex = value;
    }

    public int LoadPersonPackIndex()
    {
        return SaveData.PersonPackIndex;
    }
    public async Task Save()
    {
        await saveHelper.SaveModel(SaveData);
    }


    public void ClearSaveModel()
    {
        SaveData.Quests.Clear();
        //SaveModel.CaravanPosition.Clear();  //караван сохраняется в нулевых позиях в лагере из-за удаления старых координат
        SaveData.Relations.Clear();
        SaveData.Triggers.Clear();
        SaveData.HappenReplacements.Clear();
        SaveData.CampQuestTrigges.Clear();
        SaveData.ParamTimers.Clear();
        SaveData.ActivedHappenings.Clear();
    }
}
