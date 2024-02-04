using System;
using Zenject;
using UnityEngine;
using Assets.Modules.UI;
using Assets.Game.Plot.Scripts;
using Assets.Modules;
using GameSystems.Modules;

namespace Assets.GameEngine.Menu.Settings
{
    internal class SettingController : IInitializable, 
        ISceneReady, 
        ISceneFinish
    {
        private readonly SimpleButton backButton;
        private readonly SoundSettingView sliderView;
        private readonly SoundSettingView musicSliderView;
        private readonly MySceneManager sceneManager;
        private readonly MusicManager musicManager;
        private readonly SignalBus signalBus;

        public SettingController(MySceneManager sceneManager, MusicManager musicManager, SignalBus signalBus, [Inject(Id = "sliderView")] SoundSettingView sliderView,
            [Inject(Id = "musicSliderView")] SoundSettingView musicSliderView, SimpleButton backButton)
        {
            this.sliderView = sliderView;
            this.musicSliderView = musicSliderView;
            this.backButton = backButton;
            this.sceneManager = sceneManager;
            this.musicManager = musicManager;
            this.signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });                    
        }

        void ISceneReady.ReadyScene()
        {
            sliderView.SetSound(musicManager.SoundValue);
            musicSliderView.SetSound(musicManager.MusicValue);

            backButton.OnClick += BackHandler;
            sliderView.AddListener(musicManager.ChangeSound);
            musicSliderView.AddListener(musicManager.SetMusicVolumeImmediately);
        }
        void ISceneFinish.FinishScene()
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
