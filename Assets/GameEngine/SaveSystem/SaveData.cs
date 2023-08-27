using Assets.Game;
using Assets.Game.HappeningSystem;
using Assets.Game.Plot.Core;
using Assets.Save;
using Model.Entities;
using Model.Types;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SaveData 
{
    public Dictionary<string, Quest> Quests { get; set; } = new Dictionary<string, Quest>();
    public Dictionary<Scene, List<CaravanData>> CaravanPositions { get; set; } = new Dictionary<Scene, List<CaravanData>>();
    public Dictionary<string, int> Relations { get; set; } = new Dictionary<string, int>();
    public Dictionary<Scene, Dictionary<int, bool>> Triggers { get; set; } = new Dictionary<Scene, Dictionary<int, bool>>();
    public Dictionary<string, string> HappenReplacements { get; set; } = new Dictionary<string, string>();  
    public Dictionary<Scene, Dictionary<int, CampQuestTriggerInfo>> CampQuestTrigges { get; set; } = new Dictionary<Scene, Dictionary<int, CampQuestTriggerInfo>>();
    public Dictionary<ParameterType, float> Parameters { get; set; } = new Dictionary<ParameterType, float>()
    {
        {ParameterType.Food, 0 },
        {ParameterType.Stamina, 0 },
        {ParameterType.Spirit, 0 },
        {ParameterType.People, 0 },
    };
    public BackgroundPack BackgroundPack { get; set; }
    public string CampImagePrefab { get; set; }
    public int CampDialogAvailbale { get; set; }
    public List<string> CampDialogs { get; set; } = new List<string>();
    public Scene Scene { get; set; }
    public Scene TravelScene { get; set; }
    public float Time { get; set; } 
    public NarrativeData PlotStep { get; set; }
    public NarrativeData TutorialStep { get; set; }
    public Dictionary<ParameterType, (TimerState, float currentTime, float duration)> ParamTimers { get; set; } 
        = new Dictionary<ParameterType, (TimerState, float currentTime, float duration)>();
    public List<string> ActivedHappenings { get; set; } = new List<string>();
    public int PersonPackIndex { get; set; }
}


[Serializable]
public class NarrativeData
{
    public int CurrentStep { get; set; }
    public bool IsComplete { get; set; }
    public int LastShowStep { get; set; }
}
