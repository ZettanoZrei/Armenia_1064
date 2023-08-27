using Assets.Game.HappeningSystem;
using Assets.Game.HappeningSystem.Persons;
using Assets.Game.Parameters;
using Model.Entities.Answers;
using System;
using System.Collections;
using Zenject;

class ConsequencesController : IInitializable, ILateDisposable
{
    private readonly ConsequencesHandler consequencesHandler;
    private readonly QuestManager questManager;
    private readonly RelationManager relationManager;
    private readonly HappeningReplaceManager replaceManager;
    private readonly ParametersManager parametersManager;

    public ConsequencesController(ConsequencesHandler consequencesHandler, QuestManager questManager, 
        RelationManager relationManager, HappeningReplaceManager replaceManager, ParametersManager parametersManager)
    {
        this.consequencesHandler = consequencesHandler;
        this.questManager = questManager;
        this.relationManager = relationManager;
        this.replaceManager = replaceManager;
        this.parametersManager = parametersManager;
    }

    void IInitializable.Initialize()
    {       
        consequencesHandler.OnQuestsConsequences += questManager.WriteDownQuestConsequences;
        consequencesHandler.OnRelationConsequences += relationManager.ChangeRelation;
        consequencesHandler.OnHappeningConsequences += replaceManager.AddHappeningRaplacement;
        consequencesHandler.OnParameterConsequences += parametersManager.ChangeParameter;
    }



    void ILateDisposable.LateDispose()
    {
        consequencesHandler.OnQuestsConsequences -= questManager.WriteDownQuestConsequences;
        consequencesHandler.OnRelationConsequences -= relationManager.ChangeRelation;
        consequencesHandler.OnHappeningConsequences -= replaceManager.AddHappeningRaplacement;
        consequencesHandler.OnParameterConsequences -= parametersManager.ChangeParameter;
    }    
}


