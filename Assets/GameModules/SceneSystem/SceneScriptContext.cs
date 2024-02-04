using GameSystems.Modules;
using System.Collections.Generic;
using Zenject;

namespace Assets.Modules
{

    public class SceneScriptContext: IInitializable, ISceneFinish
    {
        private readonly List<IGameElement> elements = new List<IGameElement>();
        private readonly SignalBus signalBus;

        public SceneScriptContext(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            elements.Add(this);
        }

        public List<IGameElement> Elements => elements;

        void IInitializable.Initialize()
        {
            signalBus.Subscribe<ConnectGameElementEvent>(AddElement);
        }

        void ISceneFinish.FinishScene()
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
