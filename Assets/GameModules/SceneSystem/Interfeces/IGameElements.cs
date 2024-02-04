using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystems.Modules
{
    public interface IGameElement
    {
    }

    public interface ISceneInitialize : IGameElement
    {
        void InitScene();
    }

    public interface ISceneReady : IGameElement
    {
        void ReadyScene();
    }
    public interface ISceneStart : IGameElement
    {
        void StartScene();
    }

    public interface ISceneFinish : IGameElement
    {
        void FinishScene();
    }

    public interface IScenePause : IGameElement
    {
        void PauseScene();
    }

    public interface ISceneResume : IGameElement
    {
        void ResumeScene();
    }
}
