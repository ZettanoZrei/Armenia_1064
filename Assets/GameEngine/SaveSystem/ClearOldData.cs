using Assets.Game.Camp;
using Assets.Game.HappeningSystem.Persons;
using Assets.Game.HappeningSystem;
using Assets.Game.Parameters;
using Assets.Game.Parameters.EndedParamSystem;
using Entities;
using Zenject;
using Assets.Save;
using GameSystems.Modules;
using Assets.Modules;

namespace Loader
{
    class ClearOldData : ISceneReady, IInitializable
    {
        private readonly SignalBus signalBus;
        private readonly QuestManager questManager;
        private readonly RelationManager relationManager;
        private readonly ParametersManager parametersManager;
        private readonly EndedParamMechanics endedParamMechanics;
        private readonly BSRepositoryTrigger repositoryTriggers;
        private readonly BSRepositoryCampQuestTrigger repositoryCampQuests;
        private readonly BSRepositoryCaravan repositoryCaravan;

        public ClearOldData(SignalBus signalBus, QuestManager questManager, RelationManager relationManager, ParametersManager parametersManager, 
             EndedParamMechanics endedParamMechanics, BSRepositoryTrigger repositoryTriggers,
            BSRepositoryCampQuestTrigger repositoryCampQuests, BSRepositoryCaravan repositoryCaravan)
        {
            this.signalBus = signalBus;
            this.questManager = questManager;
            this.relationManager = relationManager;
            this.parametersManager = parametersManager;
            this.endedParamMechanics = endedParamMechanics;
            this.repositoryTriggers = repositoryTriggers;
            this.repositoryCampQuests = repositoryCampQuests;
            this.repositoryCaravan = repositoryCaravan;
        }
        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        void ISceneReady.ReadyScene()
        {
            Clear();
        }
        private void Clear()
        {
            questManager.Clear();
            relationManager.Clear();
            parametersManager.Clear();
            endedParamMechanics.Clear();
            repositoryCaravan.Clear();
            repositoryTriggers.Clear();
            repositoryCampQuests.Clear();
        }      
    }
}
