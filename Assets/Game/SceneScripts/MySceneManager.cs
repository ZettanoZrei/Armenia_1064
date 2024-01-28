using GameSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager
{
    public event Action<Scene> OnChangeScene_Post;
    public event Action OnOutOfTravelScene;
    public event Func<Task> OnToMenuScene;
    public event Action OnToTravelScene;

    public Scene CurrentScene => GetCurrentScene();
    public Scene PreviousScene { get; private set; }

    private readonly List<Scene> travelScenes = new List<Scene>{ Scene.Prologue_0, Scene.Travel_1, Scene.Travel_2 };
    private readonly List<Scene> systemScenes = new List<Scene>{ Scene.LoadScene, Scene.MainMenuScene, Scene.SettingsScene };
    public bool IsTravelScene(Scene scene) => travelScenes.Contains(scene);
    public bool IsSystemSceme(Scene scene) => systemScenes.Contains(scene);

    public static void LoadSceneForBegin(Scene name)
    {
        SceneManager.LoadScene(name.ToString());
    }

    public void LoadScene(Scene name)
    {
        InvoleAllEvents(name);
        PreviousScene = CurrentScene;
        SceneManager.LoadScene(name.ToString());
    }
    public void LoadCamp()
    {
        LoadScene(Scene.CampScene);
    }

    public void LoadTravel(Scene scene)
    {       
        LoadScene(scene);
    }

    public void LoadDialogScene()
    {
        LoadScene(Scene.DialogScene);
    }

    public void LoadMainMenuScene()
    {        
        LoadScene(Scene.MainMenuScene);
    }

    public void LoadPlotScene()
    {
        LoadScene(Scene.PlotScene);
    }

    public void LoadSettings()
    {
        LoadScene(Scene.SettingsScene);
    }
    private Scene GetCurrentScene()
    {
        var stringScene = SceneManager.GetActiveScene().name;
        foreach (Scene scene in Enum.GetValues(typeof(Scene)))
        {
            if (scene.ToString() == stringScene)
                return scene;
        }
        throw new Exception("unknown scene!!!");
    }

    #region invoke events

    private void InvoleAllEvents(Scene toScene)
    {
        if (IsTravelScene(CurrentScene))
            OnOutOfTravelScene?.Invoke();

        if (toScene == Scene.MainMenuScene)
        {
            OnToMenuScene?.Invoke();
        }
        else if(IsTravelScene(toScene))
        {
            OnToTravelScene?.Invoke();
        }
    }
    public void InvokeOnChangeScene(Scene scene)
    {
        OnChangeScene_Post?.Invoke(scene);
    }
    #endregion
}
