using Assets.Game.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Audio.Scripts
{
    internal class Debug_Music : MonoBehaviour
    {
        [Inject] private MusicMechanics musicMechanics;
        [Inject] SoundConfig soundConfig;

        public Music music;

        [ContextMenu("Play")]
        public void Play()
        {
            if (music == Music.Title)
            {
                musicMechanics.SwitchOnMusic(soundConfig.title);
            }
            else if(music == Music.Ambient) 
            {
                musicMechanics.SwitchOnMusic(soundConfig.ambient);
            }
            else if (music == Music.Tense)
            {
                musicMechanics.SwitchOnMusic(soundConfig.tense);
            }
        }


        public enum Music
        {
            Title,
            Tense,
            Ambient
        }
    }
}
