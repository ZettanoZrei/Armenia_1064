using Assets.Game.Core;
using Assets.Game.Intro;
using Assets.Modules;
using GameSystems.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.GameEngine.LoadTasks.Core
{
    internal class LoadTaskManager : StepManager<LoadStepType>,
        IInitializable,
        ISceneStart,
        ISceneReady,
        ISceneFinish
    {
        private readonly SignalBus signalBus;

        public LoadTaskManager(List<IStep<LoadStepType>> steps, SignalBus signalBus, LoadTaskSettings settings) : base(steps)
        {
            this.signalBus = signalBus;
            this.config = settings;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
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
