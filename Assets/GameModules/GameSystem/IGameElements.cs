using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystems
{
    public interface IGameElement
    {
    }

    public interface IGameElementsGroup : IGameElement
    {
        IEnumerable<T> GetElements<T>();
    }
    public interface IGameInitElement : IGameElement
    {
        void InitGame(IGameSystem gameSystem);
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

    public interface IGameChangeSceneElement : IGameElement
    {
        void ChangeScene();
    }
}
