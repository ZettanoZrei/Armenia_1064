using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.HappeningSystem;
using Assets.Game.Intro;
using Assets.Game.Plot.Core;
using Model.Entities.Happenings;
using System;
using Zenject;

namespace Assets.Game.Plot.Scripts
{
    internal class MusicManager : IInitializable, ILateDisposable
    {
        private readonly PlotManager plotManager;
        private readonly MusicMechanics musicMechancis;
        private readonly MySceneManager sceneManager;
        private readonly SoundConfig soundConfig;
        private readonly HappeningManager happeningManager;
        private readonly IntroManager introManager;

        public float SoundValue => soundConfig.volumeSounds;
        public float MusicValue => soundConfig.volumeMusic;

        public MusicManager(PlotManager plotManager, MusicMechanics musicMechancis, MySceneManager sceneManager, SoundConfig soundConfig,
            HappeningManager happeningManager, IntroManager introManager)
        {
            this.plotManager = plotManager;
            this.musicMechancis = musicMechancis;
            this.sceneManager = sceneManager;
            this.soundConfig = soundConfig;
            this.happeningManager = happeningManager;
            this.introManager = introManager;
        }

        void IInitializable.Initialize()
        {
            if (!soundConfig.activateMusic) return;

            plotManager.OnShowStep += ChooseAudio;
            sceneManager.OnChangeScene_Post += SwitchMusicForScene;
            happeningManager.OnLaunchHappening += SwitchCauseLaunchEvent;
            happeningManager.OnFinishHappening += SwitchCauseFinishEvent;
            introManager.OnShowStep += SwitchCauseIntro;

        }

        void ILateDisposable.LateDispose()
        {
            plotManager.OnShowStep -= ChooseAudio;
            sceneManager.OnChangeScene_Post -= SwitchMusicForScene;
            happeningManager.OnLaunchHappening -= SwitchCauseLaunchEvent;
            happeningManager.OnFinishHappening -= SwitchCauseFinishEvent;
            introManager.OnShowStep -= SwitchCauseIntro;
        }

        public void ChangeSound(float value)
        {
            soundConfig.volumeSounds = value;
        }
        public void SetMusicVolumeImmediately(float volume)
        {
            musicMechancis.SetVolumeImmediately(volume);
        }
        
        public void SwitchMusicForScene(Scene scene)
        {
            switch (scene)
            {
                case Scene.MainMenuScene:
                    musicMechancis.SwitchOnMusic(soundConfig.title, 0.3f, 0.15f);
                    break;
                case Scene.Prologue_0:
                    musicMechancis.SwitchOnMusic(soundConfig.ambient, 0.3f);
                    break;
                case Scene.Travel_1:
                    musicMechancis.SwitchOnMusic(soundConfig.ambient, 0.3f);
                    musicMechancis.SetVolume(soundConfig.volumeMusic, 0.2f);
                    break;
                case Scene.Travel_2:
                    musicMechancis.SwitchOnMusic(soundConfig.ambient, 0.3f);
                    musicMechancis.SetVolume(soundConfig.volumeMusic, 0.2f);
                    break;
                case Scene.CampScene: 
                    musicMechancis.SetVolume(soundConfig.volumeMusic / 3, 0.2f);
                    break;
                case Scene.DialogScene:
                    musicMechancis.SetVolume(soundConfig.volumeMusic / 3, 0.2f);
                    break;
                default: break;
            }
        }
        public void SwitchCauseFinishEvent(Happening happening)
        {
            switch (happening.Title)
            {
                case "Шаваршан_Диалог":
                    musicMechancis.SwitchOnMusic(soundConfig.title);
                    break;
                case "Востан_Диалог":
                    musicMechancis.SwitchOnMusic(soundConfig.ambient);
                    break;
            }
        }

        public void SwitchCauseLaunchEvent(Happening happening)
        {
            if (happening.Title == "Востан_Событие")
            {
                musicMechancis.SwitchOnMusic(soundConfig.tense, 0.3f);
            }
        }

        private void SwitchCauseIntro(INarrativeStep<IntroStepType> step)
        {
            if (step.StepType == IntroStepType.History)
            {
                musicMechancis.SwitchOnMusic(soundConfig.title, 0.3f, 0.15f);
            }
        }
        private void ChooseAudio(INarrativeStep<PlotStepType> step)
        {
            switch (step.StepType)
            {
                case PlotStepType.ShowMap:
                    musicMechancis.SwitchOnMusic(soundConfig.tense, 0.7f, 0.2f);
                    break;
                case PlotStepType.BeginAttack_2:
                case PlotStepType.PreSiege:
                case PlotStepType.SiegeAbout:
                case PlotStepType.AfterSiege:
                case PlotStepType.Storming:
                case PlotStepType.GameTitle:
                    musicMechancis.SwitchOnMusic(soundConfig.tense);
                    break;
                case PlotStepType.Siege:
                    musicMechancis.SwitchOnMusic(soundConfig.tense);
                    musicMechancis.SetVolume(soundConfig.volumeMusic);
                    break;
                case PlotStepType.LeaveCastle:
                    musicMechancis.SwitchOnMusic(soundConfig.ambient, 0.2f);
                    break;
                default: break;
            }
        }
    }

}
