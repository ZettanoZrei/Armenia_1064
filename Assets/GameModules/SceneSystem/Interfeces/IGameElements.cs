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

    public interface IGamePause : IGameElement
    {
        void PauseGame();
    }

    public interface IGameResume : IGameElement
    {
        void ResumeGame();
    }

    /// <summary>
    /// Выполнятеся при начале процесса игры (не входа в программу, а именно игры)
    /// </summary>
    public interface IGameStart : IGameElement //TODO: применить
    {
        void StartGame();
    }


    /// <summary>
    /// Выход из процесса игры в главное меню
    /// </summary>
    public interface IGameLeave : IGameElement //TODO: применить
    {
        void LeaveGame();
    }
}
