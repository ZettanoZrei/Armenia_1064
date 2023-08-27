using Assets.Game.Configurations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(AudioSource), typeof(Button))]
public class ButtonAudio : MonoBehaviour, IPointerDownHandler
{
    private AudioSource audioSource;
    private SoundConfig currentConfig;
    [SerializeField] private GameSettingsInstaller gameSettingsInstaller;

    [Inject]
    public void Construct(SoundConfig soundConfig)
    {
        //this is crutch. Actually I  have to create menu Options by factory
        currentConfig = soundConfig;
    }
    private void OnEnable()
    {
        this.audioSource = GetComponent<AudioSource>();

        currentConfig = currentConfig != null ? currentConfig : gameSettingsInstaller.soundConfig;
        
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Settings();
        Play();
    }

    private void Settings()
    {
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.pitch = 3;
        audioSource.volume = currentConfig.volumeSounds;
        audioSource.clip = currentConfig.buttonClick;
    }

    private void Play()
    {
        if (currentConfig.activateSounds == false)
            return;
        audioSource.Play();
    }



}
