using GameSystems.Modules;
using System.Collections.Generic;
using Zenject;

namespace Assets.Modules
{
    //TODO: Для глобального контекста FinishScene не сработает ведь там нет менеджера
    public class ScriptContext : ISceneFinish<Scene>
    {
        private readonly List<IGameElement> elements = new List<IGameElement>();
        private readonly SignalBus signalBus;

        public ScriptContext(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            signalBus.Subscribe<ConnectGameElementEvent>(AddElement);
        }

        public List<IGameElement> Elements => elements;

        public Scene Scene => Scene.LoadScene;

        public void FinishScene()
        {
            signalBus.Unsubscribe<ConnectGameElementEvent>(AddElement);
        }

        private void AddElement(ConnectGameElementEvent e)
        {
            if (!elements.Contains(e.GameElement))
            {
                elements.Add(e.GameElement);
            }
        }
    }
}
