using GameSystems.Modules;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Modules
{
    public class SceneScriptManager 
    {
        private readonly IEnumerable<ScriptContext> sceneContexts;
        public SceneState GameState { get; private set; }

        public SceneScriptManager(IEnumerable<ScriptContext> sceneContexts)
        {
            this.sceneContexts = sceneContexts;
        }

       
        public void InitScene()
        {
            GameState = SceneState.Init;
            foreach (var context in sceneContexts)
            {
                for (var i = 0; i < context.Elements.Count; i++)
                {
                    if (context.Elements[i] is ISceneInitialize gameElement)
                    {
                        gameElement.InitScene();
                    }
                }
            }
            
        }
        public void ReadyScene()
        {
            GameState = SceneState.Ready;
            foreach (var context in sceneContexts)
            {
                for (var i = 0; i < context.Elements.Count; i++)
                {
                    if (context.Elements[i] is ISceneReady gameElement)
                    {
                        gameElement.ReadyScene();
                    }
                }
            }
               
        }
        public void StartScene()
        {
            GameState = SceneState.Played;
            foreach (var context in sceneContexts)
            {
                for (var i = 0; i < context.Elements.Count; i++)
                {
                    if (context.Elements[i] is ISceneStart gameElement)
                    {
                        gameElement.StartScene();
                    }
                }
            }               
        }
        public void FinishScene()
        {
            GameState = SceneState.Finished;
            foreach (var context in sceneContexts)
            {
                for (var i = 0; i < context.Elements.Count; i++)
                {
                    if (context.Elements[i] is ISceneFinish gameElement)
                    {
                        gameElement.FinishScene();
                    }
                }
            }
            
        }
        public void PauseGame()
        {
            GameState = SceneState.Pause;
            foreach (var context in sceneContexts)
            {
                for (var i = 0; i < context.Elements.Count; i++)
                {
                    if (context.Elements[i] is IGamePause gameElement)
                    {
                        gameElement.PauseGame();
                    }
                }
            }
            
        }
        public void ResumeGame()
        {
            GameState = SceneState.Played;
            foreach (var context in sceneContexts)
            {
                for (var i = 0; i < context.Elements.Count; i++)
                {
                    if (context.Elements[i] is IGameResume gameElement)
                    {
                        gameElement.ResumeGame();
                    }
                }
            }
            
        }
    }
}
