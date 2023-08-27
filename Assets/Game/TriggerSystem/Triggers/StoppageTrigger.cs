using Assets.Game.HappeningSystem;
using Entities;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Stoppage
{
    public class StoppageTrigger : BaseFiniteTrigger
    {
        public event Action OnCaravanCollision;

        [SerializeField] private MonoEntity campPref;
        [SerializeField] private bool isForced;
        [SerializeField] private int dialogAvailable = 1;
        public bool IsForced => isForced;
        public int DialogAvailable => dialogAvailable;
        public string CampBackground => campPref != null ? campPref.name : string.Empty;

        protected override void EnterCaravanAction()
        {
            OnCaravanCollision?.Invoke();
        }
    }
}
