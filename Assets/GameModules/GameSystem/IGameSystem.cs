using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystems
{
    public interface IGameSystem
    {
        void InitGame();
        void ReadyGame();
        void StartGame();
        void PauseGame();
        void ResumeGame();
        void FinishGame();
        void ChangeScene();
    }
}
