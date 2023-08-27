using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Assets.Game.Configurations
{
    [Serializable]
    public class CaravanConfig
    {
        public float speed;
        public float spendTime;

        [Range(0f, 3f)]
        public float staminaSpend;

        [Range(0, 0.01f)]
        public float foodSpendCoef;

        [Range(0,0.01f)]
        public float spiritSpendCoef;
    }
}
