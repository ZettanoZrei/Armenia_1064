using System;
using Zenject;
using UnityEngine;
using Assets.Modules.UI;
using Assets.Game.Plot.Scripts;

namespace Assets.GameEngine.Menu.Settings
{
    internal class SettingController : MonoBehaviour, IInitializable, ILateDisposable
    {
        [SerializeField] private SimpleButton backButton;
        [SerializeField] private SoundSettingView sliderView;
        [SerializeField] private SoundSettingView musicSliderView;
        private MySceneManager sceneManager;
        private MusicManager musicManager;

        [Inject]
        public void Construct(MySceneManager sceneManager, MusicManager musicManager)
        {
            this.sceneManager = sceneManager;
            this.musicManager = musicManager;
        }

        void IInitializable.Initialize()
        {
            sliderView.SetSound(musicManager.SoundValue);
            musicSliderView.SetSound(musicManager.MusicValue);

            backButton.OnClick += BackHandler;
            sliderView.AddListener(musicManager.ChangeSound);
            musicSliderView.AddListener(musicManager.SetMusicVolumeImmediately);           
        }
        void ILateDisposable.LateDispose()
        {
            backButton.OnClick -= BackHandler;
            sliderView.RemoveListener(musicManager.ChangeSound);
            musicSliderView.RemoveListener(musicManager.SetMusicVolumeImmediately);
        }
        private void BackHandler()
        {
            sceneManager.LoadMainMenuScene();
        }
    }
}
