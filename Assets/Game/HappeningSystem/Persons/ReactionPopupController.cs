using Model.Entities.Answers;
using System;
using Zenject;

class ReactionPopupController : IInitializable, ILateDisposable
{
    private readonly ConsequencesHandler consequencesHandler;
    private readonly ReactionPopupManager reactionPopupManager;

    public ReactionPopupController(ConsequencesHandler consequencesHandler, ReactionPopupManager reactionPopupManager)
    {
        this.consequencesHandler = consequencesHandler;
        this.reactionPopupManager = reactionPopupManager;
    }
    void IInitializable.Initialize()
    {
        consequencesHandler.OnRelationConsequences += ShowReactionPopup;
    }

    void ILateDisposable.LateDispose()
    {
        consequencesHandler.OnRelationConsequences -= ShowReactionPopup;
    }

    private void ShowReactionPopup(SinglePersonConsequences consequences)
    {
        reactionPopupManager.StartRelactionPopup(consequences);
    }
}


