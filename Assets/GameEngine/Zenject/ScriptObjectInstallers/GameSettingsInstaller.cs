using Assets.Game.Configurations;
using Assets.Game.Core;
using Assets.Game.Plot.Core;
using Assets.Game.Tutorial.Core;
using UnityEngine;
using Zenject;


[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public ParametersConfig parametersConfig;
    public StartBackgroundsConfig startBackgroundsConfig;
    public CaravanConfig caravanConfig;
    public RestConfig restConfiguration;
    public StartSceneConfig startSceneConfig;
    public SoundConfig soundConfig;
    public TutorialConfig tutorialConfig;
    public PlotConfig plotConfig;
    public TimeConfig timeConfig;
    public PopupConfig popupConfig;
    public OtherConfig otherConfig;
    public SaveConfing saveConfing;
    public IntroConfig introConfig;
    public LogConfig logConfig;


    public override void InstallBindings()
    {
        Container.BindInstances(caravanConfig, parametersConfig, startBackgroundsConfig, restConfiguration,
            startSceneConfig, soundConfig, tutorialConfig, plotConfig, timeConfig, popupConfig, otherConfig, saveConfing, introConfig, logConfig);

        //Container.Bind<INarrativeConfig>().To<TutorialConfig>().FromInstance(tutorialConfig);
    }

    public void SetReleaseState()
    {
        caravanConfig.speed = 3.3f;
        startSceneConfig.fullStart = true;
        startSceneConfig.startScene = Scene.Prologue_0;
        soundConfig.activateMusic = true;
        soundConfig.activateSounds = true;
        tutorialConfig.activate = true;
        tutorialConfig.startStep = TutorialStepType.CheckCastle;
        plotConfig.activate = true;
        plotConfig.startStep = PlotStepType.ChangeStartScene;
        introConfig.activate = true;
        saveConfing.savesNumber = 1;
        saveConfing.isSave = true;
    }
}