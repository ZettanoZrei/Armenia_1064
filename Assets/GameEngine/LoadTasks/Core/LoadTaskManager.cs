using Assets.Game.Core;
using Assets.Game.Intro;
using GameSystems.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameEngine.LoadTasks.Core
{
    internal class LoadTaskManager : StepManager<LoadStepType>, 
        ISceneStart, 
        ISceneReady, 
        ISceneFinish
    {
        public LoadTaskManager(List<IStep<LoadStepType>> steps) : base(steps)
        {
        }
        void ISceneReady.ReadyScene()
        {
            Init();
        }

        void ISceneStart.StartScene()
        {
            Begin();
        }
        void ISceneFinish.FinishScene()
        {
            Finish();
        }       
    }
}
