using GameSystems.Modules;
using System.Collections.Generic;
using System.Xml.Linq;
using Zenject;

namespace Assets.Modules
{
    //TODO: ISceneFinish будет срабатывать каждую сцену для глобального контекста, это костыль
    public class ScriptContext: ISceneFinish
    {
        private readonly List<IGameElement> elements = new List<IGameElement>();
        protected readonly SignalBus signalBus;

        public ScriptContext(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            elements.Add(this);
            signalBus.Subscribe<ConnectGameElementEvent>(AddElement);
        }

        public List<IGameElement> Elements => elements;
        void ISceneFinish.FinishScene()
        {
            signalBus.TryUnsubscribe<ConnectGameElementEvent>(AddElement);
        }

        protected void AddElement(ConnectGameElementEvent e)
        {
            if (!elements.Contains(e.GameElement))
            {
                elements.Add(e.GameElement);
            }
        }      
    }
}
