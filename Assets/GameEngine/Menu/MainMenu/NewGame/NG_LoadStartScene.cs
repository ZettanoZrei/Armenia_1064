using Assets.Game.Configurations;
using UnityEngine;

class NG_LoadStartScene : ING_Task
{
    private readonly MySceneManager sceneManager;
    private readonly StartSceneConfig startSceneConfig;

    public NG_LoadStartScene(MySceneManager sceneManager, ConfigurationRuntime configurationRuntime)
    {
        this.sceneManager = sceneManager;
        this.startSceneConfig = configurationRuntime.StartSceneConfig;
    }

    void ING_Task.Execute()
    {
        Time.timeScale = 1;
        sceneManager.LoadTravel(startSceneConfig.startScene);
    }
}
