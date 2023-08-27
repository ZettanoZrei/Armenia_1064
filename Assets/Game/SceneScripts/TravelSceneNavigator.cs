using Assets.Save;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Zenject;

public class TravelSceneNavigator : ILateDisposable
{
    private readonly MySceneManager sceneManager;
    private IEnumerable<NextTravelSceneTrigger> sceneTriggers;
    private Scene scenePointer;
    public Scene ScenePointer => scenePointer;

    private bool isNextTravelScene;
    public TravelSceneNavigator(MySceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
    }
    public void SetTriggers(IEnumerable<NextTravelSceneTrigger> sceneTriggers)
    {
        this.sceneTriggers = sceneTriggers;
        foreach (var trigger in sceneTriggers)
        {
            trigger.OnNextScene += SetNextScene;
            trigger.OnNextScene += SetIsNextTravelScene;
        }
    }

    public void LoadTravelScene()
    {
        if (isNextTravelScene)
        {
            //CleanBSRepos();
            isNextTravelScene = false;
        }
        sceneManager.LoadTravel(scenePointer);
    }

    public void SetNextScene(Scene scene)
    {
        this.scenePointer = scene;
    }

    private void SetIsNextTravelScene(Scene _)
    {
        isNextTravelScene = true;
    }

    //private void CleanBSRepos()
    //{
    //    MonoBehaviour.print("repositoryCaravan");
    //    repositoryCaravan.Clear();
    //    repositoryTriggers.Clear();
    //    repositoryCampQuests.Clear();
    //}
    void ILateDisposable.LateDispose()
    {
        foreach (var trigger in sceneTriggers)
        {
            trigger.OnNextScene -= SetNextScene;
            trigger.OnNextScene -= SetIsNextTravelScene;
        }
    }
}
