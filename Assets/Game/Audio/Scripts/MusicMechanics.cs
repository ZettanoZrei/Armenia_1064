using Assets.Game.Configurations;
using Assets.Game.HappeningSystem;
using Entities;
using Model.Entities.Happenings;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

class MusicMechanics
{
    private readonly SoundConfig soundConfig;
    private readonly AudioSource audioSource;
    private CompositeDisposable playDisposables = new CompositeDisposable();
    private CompositeDisposable transitionsDisposables = new CompositeDisposable();

    private bool isPlay;
    //private float clipTime;

    private Dictionary<AudioClip, float> records = new Dictionary<AudioClip, float>();
    public MusicMechanics(SoundConfig soundConfig, AudioSource audioSource)
    {
        this.soundConfig = soundConfig;
        this.audioSource = audioSource;

        SetSettings();
        MusicRecord();
    }

    public void MusicRecord()
    {
        Observable.Timer(TimeSpan.FromSeconds(1))
            .Repeat()
            .Where(_ => isPlay == true && soundConfig.activateMusic == true)
            .Subscribe(record =>
            {
                records[audioSource.clip] = audioSource.time;
            });
    }

    public void SwitchOnMusic(AudioClip audioClip, float speedCoef = 1, float maxVolume = 0.35f)
    {
        var volume = Math.Min(maxVolume, soundConfig.volumeMusic);
        if (soundConfig.activateMusic == false || audioClip == audioSource.clip)
            return;

        if (isPlay)
            Stop();

        Observable.EveryUpdate()
            .Where(_ => isPlay == false)
            .Subscribe(_ =>
            {

                SetAudioClip(audioClip);
                Play(speedCoef, volume);
                transitionsDisposables.Clear();
            })
            .AddTo(transitionsDisposables);
    }

    public void SwitchOffMusic(float speedCoef = 1)
    {
        if (!soundConfig.activateMusic)
            return;

        Stop(speedCoef);
    }

    public void SetVolumeImmediately(float newVolume)
    {
        soundConfig.volumeMusic = newVolume;
        audioSource.volume = newVolume;
    }
    public void SetVolume(float newVolume, float speedCoef = 1)
    {
        Observable.Timer(TimeSpan.FromSeconds(0.3), Scheduler.MainThreadIgnoreTimeScale)
            .Repeat()
            .Subscribe(_ =>
            {
                if (newVolume > audioSource.volume)
                {
                    audioSource.volume += soundConfig.speedUp * speedCoef;
                    Debug.Log($"volume: {audioSource.volume}");
                    if (audioSource.volume >= newVolume)
                    {
                        playDisposables.Clear();
                    }
                }
                else if (newVolume < audioSource.volume)
                {
                    audioSource.volume -= soundConfig.speedDown * speedCoef;
                    Debug.Log($"volume: {audioSource.volume}");
                    if (audioSource.volume <= newVolume)
                    {
                        playDisposables.Clear();
                    }
                }

            })
            .AddTo(playDisposables);
    }
    private void SetSettings()
    {
        audioSource.playOnAwake = false;
        audioSource.loop = true;
        audioSource.volume = 0f;
    }



    private void Play(float speedCoef, float maxVolume)
    {
        audioSource.Play();
        isPlay = true;
        Observable.Timer(TimeSpan.FromSeconds(0.3), Scheduler.MainThreadIgnoreTimeScale)
            .Repeat()
            .Subscribe(_ =>
            {
                
                if (audioSource.volume >= maxVolume)
                {
                    playDisposables.Clear();
                }
                else
                {
                    audioSource.volume += soundConfig.speedUp * speedCoef;
                    //Debug.Log($"volume: {audioSource.volume}");
                }
            })
            .AddTo(playDisposables);
    }

    private void Stop(float coef = 1)
    {
        playDisposables.Clear();
        transitionsDisposables.Clear();
        Observable.Timer(TimeSpan.FromSeconds(0.3), Scheduler.MainThreadIgnoreTimeScale)
            .Repeat()
            .Subscribe(_ =>
            {
                audioSource.volume -= soundConfig.speedDown * coef;
                //Debug.Log($"volume: {audioSource.volume}");
                if (audioSource.volume <= 0)
                {
                    playDisposables.Clear();
                    isPlay = false;
                }

            })
            .AddTo(playDisposables);
    }

    private void SetAudioClip(AudioClip audioClip)
    {
        try
        {
            audioSource.clip = audioClip;
            if (records.ContainsKey(audioClip))
            {
                try
                {
                    audioSource.time = records[audioClip];
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex.ToString());
                    Debug.Log(ex.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteLog(ex.ToString());
        }
    }
}


