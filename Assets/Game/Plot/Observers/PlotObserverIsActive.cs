using Assets.Game.Plot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Plot.Observers
{
    internal class PlotObserverIsActive : PlotObserver
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
