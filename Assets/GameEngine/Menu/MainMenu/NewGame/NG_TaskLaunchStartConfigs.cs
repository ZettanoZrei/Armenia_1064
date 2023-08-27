using Assets.Game;
using Assets.Game.Camp;
using Assets.Game.Configurations;
using Assets.Game.Parameters;
using Assets.Game.Timer;
using Assets.Game.Tutorial.Core;
using Model.Types;

namespace Loader
{
    class NG_TaskLaunchStartConfigs : ING_Task
    {
        private readonly DialogBackgroundKeeper backgroundManager;
        private readonly StartBackgroundsConfig startParameters;
        private readonly ConfigurationRuntime configurationRuntime;
        private readonly StartSceneConfig startSceneConfig;
        private readonly TravelSceneNavigator travelSceneNavigator;
        private readonly CampIncomingData campIncomingData;
        private readonly ParametersConfig srartParameters;
        private readonly ParametersManager parametersManager;
        private readonly TimeMechanics timeManager;
        private readonly PlotConfig plotConfig;
        private readonly TutorialConfig tutorialConfig;
        

        public NG_TaskLaunchStartConfigs(DialogBackgroundKeeper backgroundManager, StartBackgroundsConfig startParameters, ConfigurationRuntime configurationRuntime,
            TravelSceneNavigator travelSceneNavigator, CampIncomingData campIncomingData, ParametersConfig srartParameters, ParametersManager parametersManager,
            TimeMechanics timeManager, PlotConfig plotConfig, TutorialConfig tutorialConfig, StartSceneConfig startSceneConfig)
        {
            this.backgroundManager = backgroundManager;
            this.startParameters = startParameters;
            this.configurationRuntime = configurationRuntime;
            this.startSceneConfig = startSceneConfig;
            this.travelSceneNavigator = travelSceneNavigator;
            this.campIncomingData = campIncomingData;
            this.srartParameters = srartParameters;
            this.parametersManager = parametersManager;
            this.timeManager = timeManager;
            this.plotConfig = plotConfig;
            this.tutorialConfig = tutorialConfig;
        }

        void ING_Task.Execute()
        {
            campIncomingData.CampImagePrefab = startParameters.campBackground;
            backgroundManager.SetDialogBackground(startParameters.dialogBackground);
            travelSceneNavigator.SetNextScene(startSceneConfig.startScene);

            parametersManager.SetParameter(srartParameters.spirit, ParameterType.Spirit);
            parametersManager.SetParameter(srartParameters.food, ParameterType.Food);
            parametersManager.SetParameter(srartParameters.people, ParameterType.People);
            parametersManager.SetParameter(srartParameters.stamina, ParameterType.Stamina);

            timeManager.SetDays(0);

            configurationRuntime.PlotConfig.startStep = plotConfig.startStep;
            configurationRuntime.TutorialConfig.startStep = tutorialConfig.startStep;
        }
    }
}
