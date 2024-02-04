using Assets.Modules;
using GameSystems.Modules;
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
    internal class SceneStartBeacon : MonoBehaviour, IInitializable, ISceneStart
    {
        private MySceneManager sceneManager;
        private SignalBus signalBus;
        [SerializeField] private Scene currentScene;

        [Inject]
        public void Construct(MySceneManager sceneManager, SignalBus signalBus)
        {
            this.sceneManager = sceneManager;
            this.signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        void ISceneStart.StartScene()
        {
            sceneManager.InvokeOnChangeScene(currentScene);
            //StartCoroutine(DelayInvoke());
        }
        
        //IEnumerator DelayInvoke()
        //{
        //    yield return new WaitForEndOfFrame();
        //    sceneManager.InvokeOnChangeScene(currentScene);
        //}

        
    }
}
