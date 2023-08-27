using Assets.Game.Tutorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Tutorial.Observers
{
    class ObserverIsActive : TutorialObserver
    {
        [SerializeField] private GameObject observable;

        protected override void DoAfterStep()
        {
            observable.SetActive(true);
        }

        protected override void DoBeforeStep()
        {
            observable.SetActive(false);
        }
    }
}
