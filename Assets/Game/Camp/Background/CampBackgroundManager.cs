using Entities;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Game.Camp.Background
{
    public class CampBackgroundManager : MonoBehaviour, IInitializable
    {
        [SerializeField] Transform backContainer;
        [Inject] private CampIncomingData сampIncomingData;
        public CampBackground CampBackground { get; private set; }
        void IInitializable.Initialize()
        {
            var path = $"Background/Camp/{сampIncomingData.CampImagePrefab}";
            this.CampBackground = Instantiate(Resources.Load<MonoEntity>(path), backContainer).Element<CampBackground>();
        }
    }
}
