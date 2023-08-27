using System;
using UnityEngine;

namespace Assets.Game.Configurations
{
    [Serializable]
    public class SoundConfig
    {
        public AudioClip buttonClick;
        public AudioClip tense;
        public AudioClip title;
        public AudioClip ambient;
        

        public float speedUp;
        public float speedDown;
        [Range(0f, 1f)]
        public float volumeMusic;
        public bool activateMusic;

        [Range(0f, 1f)]
        public float volumeSounds;
        public bool activateSounds;
    }
}
