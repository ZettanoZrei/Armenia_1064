using Assets.Game;
using Assets.GameEngine.Menu.Settings;
using Assets.Modules.UI;
using System;
using UnityEditor;
using UnityEngine;
using Zenject;

public class MonoSettingsInstaller : MonoInstaller
{
    [SerializeField] private SoundSettingView sliderView;
    [SerializeField] private SoundSettingView musicSliderView;
    [SerializeField] private SimpleButton backButton;
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<SettingController>().FromComponentInHierarchy().AsCached();

        Container.Bind<SoundSettingView>().WithId("sliderView").FromInstance(sliderView).AsCached();
        Container.Bind<SoundSettingView>().WithId("musicSliderView").FromInstance(musicSliderView).AsCached();
        Container.Bind<SimpleButton>().FromInstance(backButton).AsCached();

        Container.BindInterfacesTo<SettingController>().AsSingle();
    }
}