using Assets.Game.DialogBackTriggers;
using Assets.Modules;
using GameSystems.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    public class FiniteTriggerCatalog : MonoBehaviour, IInitializable, ISceneReady
    {
        private List<BaseFiniteTrigger> triggers = new List<BaseFiniteTrigger>();
        private int index = 0;


        private SignalBus signalBus;

        [Inject]
        private void Construct(SignalBus signalBus, IEnumerable<ActivatorStaticTrigger> staticRoadTriggers, IEnumerable<LaunchStaticTrigger> beginHappeningTriggers,
            IEnumerable<FastPointerTrigger> fastPointerTriggers, IEnumerable<CampQuestTriggerModel> campQuestTriggerModes, IEnumerable<DialogBackTrigger> dialogBackTriggers)
        {
            this.signalBus = signalBus;

            triggers.AddRange(staticRoadTriggers);
            triggers.AddRange(beginHappeningTriggers);
            triggers.AddRange(fastPointerTriggers);
            triggers.AddRange(campQuestTriggerModes);
            triggers.AddRange(dialogBackTriggers);
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }

        void ISceneReady.ReadyScene()
        {
            SetIndex();
        }
        public IEnumerable<T> GetElements<T>() where T : BaseFiniteTrigger
        {
            var list = new List<T>();
            foreach (var trigger in triggers)
            {
                if (trigger is T result)
                {
                    list.Add(result);
                }
            }
            return list;
        }
        private void SetIndex()
        {
            foreach (var trigger in triggers)
            {
                trigger.Index = index++;
            }
        }

        
    }
}
