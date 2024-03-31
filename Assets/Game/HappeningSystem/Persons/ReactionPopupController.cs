using Model.Entities.Answers;
using System;
using Zenject;

class ReactionPopupController : IInitializable, ILateDisposable
{
    private readonly ReactionPopupManager reactionPopupManager;

    public ReactionPopupController(ReactionPopupManager reactionPopupManager)
    {
        this.reactionPopupManager = reactionPopupManager;
    }
    void IInitializable.Initialize()
    {
  
    }

    void ILateDisposable.LateDispose()
    {

    }

    private void ShowReactionPopup(SinglePersonConsequences consequences)
    {
        reactionPopupManager.StartRelactionPopup(consequences);
    }
}


