using System;
using UnityEngine;

namespace Assets.Game.Configurations
{
    [Serializable]
    public class ParametersConfig
    {
        public int food;
        public int spirit;
        public int stamina;
        public int people;

        //how many times remove people per day
        public int removePeopleTimes;

        //how many people remove per removing
        public int countPeopleRemoved;

        //gow fast proggress chenged
        public float changeSpeed;
        public Sprite whiteProgress;
    }
}
