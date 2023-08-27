using Assets.Game.Camp;
using Assets.Game.HappeningSystem.Persons;
using Assets.Game.HappeningSystem;
using Assets.Game.Parameters;
using Assets.Game.Parameters.EndedParamSystem;
using Entities;
using Zenject;
using Assets.Save;

namespace Loader
{
    class ClearOldData : IInitializable
    {
        private readonly MySceneManager sceneManager;
        private readonly QuestManager questManager;
        private readonly RelationManager relationManager;
        private readonly ParametersManager parametersManager;
        private readonly HappeningReplaceManager replaceManager;
        private readonly EndedParamMechanics endedParamMechanics;
        private readonly HappeningManager happeningManager;
        private readonly BSRepositoryTrigger repositoryTriggers;
        private readonly BSRepositoryCampQuestTrigger repositoryCampQuests;
        private readonly BSRepositoryCaravan repositoryCaravan;

        public ClearOldData(MySceneManager sceneManager, QuestManager questManager, RelationManager relationManager, ParametersManager parametersManager, 
            HappeningReplaceManager replaceManager, EndedParamMechanics endedParamMechanics, HappeningManager happeningManager, BSRepositoryTrigger repositoryTriggers,
            BSRepositoryCampQuestTrigger repositoryCampQuests, BSRepositoryCaravan repositoryCaravan)
        {
            this.sceneManager = sceneManager;
            this.questManager = questManager;
            this.relationManager = relationManager;
            this.parametersManager = parametersManager;
            this.replaceManager = replaceManager;
            this.endedParamMechanics = endedParamMechanics;
            this.happeningManager = happeningManager;
            this.repositoryTriggers = repositoryTriggers;
            this.repositoryCampQuests = repositoryCampQuests;
            this.repositoryCaravan = repositoryCaravan;
        }

        

        void IInitializable.Initialize()
        {
            sceneManager.OnChangeScene_Post += CheckMenuScene;
        }

        private void CheckMenuScene(Scene scene)
        {
            if(scene == Scene.MainMenuScene)
            {
                Clear();
            }
        }
        private void Clear()
        {
            questManager.Clear();
            relationManager.Clear();
            parametersManager.Clear();
            replaceManager.Clear();
            endedParamMechanics.Clear();
            happeningManager.Clear();

            repositoryCaravan.Clear();
            repositoryTriggers.Clear();
            repositoryCampQuests.Clear();
        }
    }
}
