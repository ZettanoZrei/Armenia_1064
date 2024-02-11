using Zenject;
using UnityEngine;
using Assets.Game.HappeningSystem;
using Loader;
using Assets.Game.HappeningSystem.View.Advice;
using Assets.Systems.SaveSystem;
using Assets.Game.HappeningSystem.Persons;
using Assets.Game;
using Assets.Game.Parameters;
using Assets.Game.Message;
using Assets.Game.Configurations;
using Assets.Save;
using Assets.Game.Camp;
using Assets.Game.Tutorial.Core;
using Assets.Game.Tutorial.Steps;
using Assets.Game.Core;
using Assets.Game.Plot.Core;
using Assets.Game.Plot.Steps;
using Assets.Game.Plot;
using Assets.Game.Plot.Scripts;
using Assets.Game.Timer;
using Assets.Game.Parameters.EndedParamSystem;
using Assets.GameSystems.SaveSystem;
using UnityEditor;
using Assets.GameEngine;
using Assets.Game.UI.EndPopupSystem;
using Assets.Game.Intro;
using Assets.Game.Intro.Step;
using Assets.Game.UI.FailGameSystem;
using Assets.GameEngine.Zenject;
using Assets.Modules;
using UnityEngine.UIElements;

public class MonoGlobalInstaller : MonoInstaller
{
    [SerializeField] private GameObject portraitButtonPrefab;
    [SerializeField] private GameObject campIconPrefab;
    [SerializeField] private PortaitHeap portaitHeapPrefab;
    [SerializeField] private GameObject audioSource;

    [SerializeField] private PlotStoryModel plotMapModel;
    [SerializeField] private PlotStoryModel plotSiegeModel;
    [SerializeField] private PlotDialogModel plotDialogModel;

    [SerializeField] private ReactionPart reactionPartPrefab;
    [SerializeField] private GameObject inputIntroController;
    private PortaitHeap uiHeap;
    private Transform core;
    public override void InstallBindings()
    {
        uiHeap = GameObject.FindWithTag("portrait_heap").GetComponent<PortaitHeap>();
        core = GameObject.FindWithTag("core").GetComponent<Transform>();

        BindQuestSystem();
        BindParameters();
        BindSaveSystem();
        BindHappeningsSystem();
        BindSceneSystems();
        BindBSRopositories();
        BindAudioSource();
        BindTutorialSystem();
        BindPlotSystem();
        BindIntroSystem();
        BindMenu();
        BindDialogSubSystem();

        Container.Bind<PortaitHeap>().FromInstance(uiHeap);
        Container.Bind<TimeMechanics>().AsSingle();
        Container.Bind<RelationManager>().AsSingle();
        Container.Bind<CampIncomingData>().AsSingle();
        Container.BindInterfacesAndSelfTo<SaveManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<RestLocker>().AsSingle();
        Container.Bind<ConfigurationRuntime>().AsSingle();
        Container.BindInterfacesTo<EndGameManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameOverManager>().AsSingle();
        Container.Bind<ShowUIElementsModel>().AsTransient();

        Container.BindHappeningSystem();

        Container.BindFactory<string, ReactionPart, ReactionPart.Factory>()
            .FromMonoPoolableMemoryPool(x => x.WithInitialSize(6).FromComponentInNewPrefab(reactionPartPrefab).UnderTransform(uiHeap.transform));

        Container.BindInterfacesAndSelfTo<ScriptContext>().AsSingle();


        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<ConnectGameElementEvent>();
    }

    private void BindDialogSubSystem()
    {
        Container.Bind<DialogPersonPackCatalog>().FromScriptableObjectResource("Entities/DialogPersonCatalog").AsSingle();
        Container.Bind<DialogBackgroundKeeper>().AsSingle();
        Container.BindInterfacesTo<DialogBackgroundController>().AsSingle();
    }

    private void BindMenu()
    {
        //main menu
        Container.Bind<LoadManager>().AsTransient();
        Container.Bind<IMenuCommand>().To<ContinueCommand>().AsTransient();
        Container.Bind<IMenuCommand>().To<NewGameCommand>().AsTransient();
        Container.Bind<IMenuCommand>().To<SettingsCommand>().AsTransient();
        Container.Bind<ING_Task>().To<NG_TaskLaunchStartConfigs>().AsTransient();
        Container.Bind<ING_Task>().To<NG_TaskLoadQuests>().AsTransient();
        Container.Bind<ING_Task>().To<NG_TaskLoadRelations>().AsTransient();
        Container.Bind<ING_Task>().To<NG_TaskInitSaveModel>().AsTransient();
        Container.Bind<ING_Task>().To<NG_TaskBeginNarrative>().AsTransient();
        Container.Bind<ING_Task>().To<NG_LoadStartScene>().AsTransient();
    }

    private void BindIntroSystem()
    {
        Container.BindInterfacesAndSelfTo<IntroManager>().AsSingle();
        Container.BindFactory<InputIntroController, InputIntroController.Factory>().FromComponentInNewPrefab(inputIntroController);

        Container.Bind<IStep<IntroStepType>>().To<IntroStep0CreateSkipController>().AsSingle();
        Container.Bind<IStep<IntroStepType>>().To<IntroStep1ShowLogo>().AsSingle();
        Container.Bind<IStep<IntroStepType>>().To<IntroStep2ShowHistory>().AsSingle();
        Container.Bind<IStep<IntroStepType>>().To<IntroStep3LaunchGame>().AsSingle();
    }
    private void BindTutorialSystem()
    {
        Container.BindInterfacesAndSelfTo<TutorialManager>().AsSingle();

        Container.Bind<IStep<TutorialStepType>>().To<StepComeInCastleCheck>().AsSingle();
        Container.Bind<IStep<TutorialStepType>>().To<StepShowDialogRelation>().AsSingle().WithArguments(PopupType.TutorRelationPopup);
        Container.Bind<IStep<TutorialStepType>>().To<StepComeFromCastleCheck>().AsSingle();
        Container.Bind<IStep<TutorialStepType>>().To<StepShowInterfaceInstructions>().AsSingle().WithArguments(PopupType.TutorInterfacePopup);
        Container.Bind<IStep<TutorialStepType>>().To<StepShowAdvices>().AsSingle().WithArguments(PopupType.TutorAdvicePopup);
        Container.Bind<IStep<TutorialStepType>>().To<StepShowRest>().AsSingle().WithArguments(PopupType.TutorRestPopup);
        Container.Bind<IStep<TutorialStepType>>().To<StepShowSetupCamp>().AsSingle().WithArguments(PopupType.TutorSetupCampPopup);
    }

    private void BindPlotSystem()
    {
        Container.BindInterfacesAndSelfTo<PlotManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlotSaveController>().AsSingle();

        Container.Bind<IStep<PlotStepType>>().To<PStep0ChangeStartScene>().AsSingle();
        Container.Bind<IStep<PlotStepType>>().To<PStep1ShowMapPopup>().AsSingle().WithArguments(plotMapModel);
        Container.Bind<IStep<PlotStepType>>().To<PStep2BeginGame>().AsSingle();
        Container.Bind<IStep<PlotStepType>>().To<PStep3ShowFirstWords>().AsSingle();
        Container.Bind<IStep<PlotStepType>>().To<PStep4ShowDialog>().AsSingle().WithArguments(plotDialogModel);        
        Container.Bind<IStep<PlotStepType>>().To<PStep5BeginAttack_1>().AsSingle();
        Container.Bind<IStep<PlotStepType>>().To<PStep5BeginAttack_2>().AsSingle();
        Container.Bind<IStep<PlotStepType>>().To<PStep6ShowPreSiege>().AsSingle();
        Container.Bind<IStep<PlotStepType>>().To<PStep7ShowSiege>().AsSingle().WithArguments(plotSiegeModel);
        Container.Bind<IStep<PlotStepType>>().To<PStep8SiegeAbout>().AsSingle();
        Container.Bind<IStep<PlotStepType>>().To<PStep9ShowAfterSiege>().AsSingle();
        Container.Bind<IStep<PlotStepType>>().To<PStep10ChangeCampSprite>().AsSingle();
        Container.Bind<IStep<PlotStepType>>().To<PStep11ChangePersonSprites>().AsSingle();
        Container.Bind<IStep<PlotStepType>>().To<PStep12Storming>().AsSingle();
        Container.Bind<IStep<PlotStepType>>().To<PStep13GameTitle>().AsSingle();
        Container.Bind<IStep<PlotStepType>>().To<PStep14LeaveCastle>().AsSingle();
    }

    
    private void BindBSRopositories()
    {
        Container.Bind<BSRepositoryCaravan>().AsSingle();
        Container.Bind<BSRepositoryTrigger>().AsSingle();
        Container.Bind<BSRepositoryCampQuestTrigger>().AsSingle();
    }
    private void BindSceneSystems()
    {
        Container.Bind<MySceneManager>().AsSingle();
        Container.Bind<SetupCampManager>().AsSingle();
        Container.Bind<TravelSceneNavigator>().AsSingle();
    }
    private void BindParameters()
    {
        Container.Bind<ParametersManager>().AsSingle();     
        //Container.BindInterfacesAndSelfTo<EndedParamController>().AsSingle();
    }
    private void BindSaveSystem()
    {
        Container.Bind<Repository>().AsSingle();
        Container.Bind<FileHelper>().AsTransient();
        Container.Bind<SaveHelper<SaveData>>().AsSingle();
        Container.BindInterfacesAndSelfTo<SaveController>().AsSingle();
    }


    private void BindQuestSystem()
    {
        Container.Bind<QuestManager>().AsSingle();
    }
    private void BindHappeningsSystem()
    {       
        Container.Bind<HappeningCatalog>().AsSingle();          
        Container.BindFactory<PortraitButton, PortraitButton.Factory>()
            .FromComponentInNewPrefab(portraitButtonPrefab)
            .UnderTransform(uiHeap.transform);      
    }    
    private void BindAudioSource()
    {        
        Container.Bind<AudioSource>().FromComponentInNewPrefab(audioSource).UnderTransform(core).AsSingle();
        Container.Bind<MusicMechanics>().AsSingle();
        Container.BindInterfacesAndSelfTo<MusicManager>().AsSingle();
    }
}
