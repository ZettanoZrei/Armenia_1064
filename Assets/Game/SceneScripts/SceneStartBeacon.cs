using GameSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.SceneScripts
{
    internal class SceneStartBeacon : MonoBehaviour, IGameStartElement
    {
        private MySceneManager sceneManager;
        [SerializeField] private Scene currentScene;

        [Inject]
        public void Construct(MySceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }
        void IGameStartElement.StartGame()
        {
            StartCoroutine(DelayInvoke());
        }

        //private void Start()
        //{
        //    sceneManager.InvokeOnChangeScene(currentScene);
        //    Debug.Log("SceneStartBeacon:" + currentScene);
        //}

        IEnumerator DelayInvoke()
        {
            yield return new WaitForEndOfFrame();
            sceneManager.InvokeOnChangeScene(currentScene);
        }
    }
}
