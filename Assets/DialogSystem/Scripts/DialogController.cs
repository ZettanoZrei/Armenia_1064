using Assets.Modules;
using GameSystems.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.DialogSystem.Scripts.Conclusion
{
    //Прослушивать все сообщения от акторов. Перенести dialog system в laod сцену
    public class DialogController : MonoBehaviour,
        IInitializable,
        ISceneReady,
        ISceneFinish
    {
        private DialogConclusionManager conclusionManager;
        private ActorEventObserver actorEventObserver;
        private DialogSequenceManager dialogSequenceManager;
        private SignalBus signalBus;

        [Inject]
        public void Construct(DialogConclusionManager conclusionManager, ActorEventObserver actorEventObserver, DialogSequenceManager dialogSequenceManager,
            SignalBus signalBus)
        {
            this.conclusionManager = conclusionManager;
            this.actorEventObserver = actorEventObserver;
            this.dialogSequenceManager = dialogSequenceManager;
            this.signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }

        void ISceneReady.ReadyScene()
        {
            actorEventObserver.DialogStartEvent += conclusionManager.CreateDialogConclusion;
            actorEventObserver.DialogEndEvent += dialogSequenceManager.ExecuteSequence;
        }
        void ISceneFinish.FinishScene()
        {
            actorEventObserver.DialogStartEvent -= conclusionManager.CreateDialogConclusion;
            actorEventObserver.DialogEndEvent -= dialogSequenceManager.ExecuteSequence;
        }        
    }
}
