using Assets.Game.Camp;
using Assets.Game.HappeningSystem;
using System;
using System.Linq;

public class SetupCampManager
{
    public event Action OnSetupCamp_Before;
    public event Action OnLeaveCamp_Before;

    private readonly MySceneManager sceneManager;
    private readonly CampIncomingData campIncomingData;
    private readonly QuestManager questManager;
    private readonly TravelSceneNavigator sceneNavigator;

    public SetupCampManager(TravelSceneNavigator sceneNavigator, MySceneManager sceneManager, CampIncomingData campIncomingData, QuestManager questManager)
    {
        this.sceneNavigator = sceneNavigator;
        this.sceneManager = sceneManager;
        this.campIncomingData = campIncomingData;
        this.questManager = questManager;
    }
    public void SetupCamp(int dialogAvailable = 1)
    {
        OnSetupCamp_Before?.Invoke();
        //if (dialogAvailable > 0) //can be less zero if i want not change amount of available dialogs
        campIncomingData.SetDialogAvailable(dialogAvailable);
        campIncomingData.CampQuests = questManager.GetAvailableCampQuest().ToList();

        Logger.WriteLog($"Setup camp: dialogAvailable - {campIncomingData.DialogAvailable}");
        sceneManager.LoadCamp();
    }

    public void LeaveCamp()
    {
        if (new CheckRequiredQuest().Execute())
        {
            OnLeaveCamp_Before?.Invoke();
            sceneNavigator.LoadTravelScene();
        }
    }
}
