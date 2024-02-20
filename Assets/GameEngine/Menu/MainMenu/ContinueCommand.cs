using Assets.Game.Plot.Core;
using Assets.Game.Tutorial.Core;
using Loader;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

class ContinueCommand : IMenuCommand
{
    private readonly MySceneManager sceneManager;
    private readonly LoadManager loadManager;
    private readonly PlotManager plotManager;
    private readonly TutorialManager tutorialManager;

    public ContinueCommand(MySceneManager sceneManager, LoadManager loadManager, PlotManager plotManager, TutorialManager tutorialManager)
    {
        this.sceneManager = sceneManager;
        this.loadManager = loadManager;
        this.plotManager = plotManager;
        this.tutorialManager = tutorialManager;
    }
    public string LoadPath { get; set; } //for debug loading game
    void IMenuCommand.Execute()
    {
        loadManager.LoadData(LoadPath);
        var scene = loadManager.LoadScene();
        sceneManager.OnChangeScene_Post += TurnOnPlot;
        sceneManager.LoadScene(scene);
    }

    private void TurnOnPlot(Scene _)
    {
        sceneManager.OnChangeScene_Post -= TurnOnPlot;
        plotManager.Begin();
        tutorialManager.Begin();
    }
}
