using Assets.Game.Plot.Core;
using Assets.Game.Tutorial.Core;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Assets.Game.UI.DebugLoading
{
    public class LoadGameButton : MonoBehaviour
    {
        [SerializeField] private Text uText;
        private string path;
        ContinueCommand continueCommand;
        MySceneManager sceneManager;
        PlotManager plotManager;
        TutorialManager tutorialManager;

        [Inject]
        private void Construct(ContinueCommand continueCommand, MySceneManager sceneManager, PlotManager plotManager,
            TutorialManager tutorialManager)
        {
            this.continueCommand = continueCommand;
            this.sceneManager = sceneManager;
            this.plotManager = plotManager;
            this.tutorialManager = tutorialManager;
        }

        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(LoadGame);
        }
        private void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveAllListeners();
        }
        public void SetPath(DateTime time, string path)
        {
            uText.text = time.ToString();
            this.path = path.ToString();
        }

        public void LoadGame() 
        {
            if (string.IsNullOrEmpty(path)) return;
            continueCommand.LoadPath = path;
            (continueCommand as IMenuCommand).Execute();

            //loadManager.LoadData(path);
            //var scene = loadManager.LoadScene();
            //sceneManager.OnChangeScene_Post += TurnOnPlot;
            //sceneManager.LoadScene(scene);
        }

        //private void TurnOnPlot(Scene _)
        //{
        //    sceneManager.OnChangeScene_Post -= TurnOnPlot;
        //    plotManager.Begin();
        //    tutorialManager.Begin();
        //}

        public class Factory : PlaceholderFactory<LoadGameButton>
        {
        }
    }
}
