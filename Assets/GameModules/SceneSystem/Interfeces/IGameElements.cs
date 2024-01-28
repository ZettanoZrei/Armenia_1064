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

    public interface IGameInitElement : IGameElement
    {
        void InitGame();
    }

    public interface IGameReadyElement : IGameElement
    {
        void ReadyGame();
    }
    public interface IGameStartElement : IGameElement
    {
        void StartGame();
    }

    public interface IGameFinishElement : IGameElement
    {
        void FinishGame();
    }

    public interface IGamePauseElement : IGameElement
    {
        void PauseGame();
    }

    public interface IGameResumeElement : IGameElement
    {
        void ResumeGame();
    }
}
