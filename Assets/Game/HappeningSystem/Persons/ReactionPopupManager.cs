using Assets.Game;
using Model.Entities.Answers;
using System.Collections.Generic;
using System.Timers;
using Assets.Game.Configurations;
using System.Linq;
using UnityEngine;
using System.Threading;
using System.ComponentModel;
using Zenject;
using Assets.GameEngine;

class ReactionPopupManager
{
    private readonly PopupManager popupManager;
    private readonly ReactionPart.Factory factory;
    private readonly PopupConfig.ReactionPopupConfig config;
    private bool isPopupActive;
    private ReactionPopup popup;

    public ReactionPopupManager(PopupManager popupManager, ReactionPart.Factory factory, PopupConfig popupConfig)
    {
        this.popupManager = popupManager;
        this.factory = factory;
        this.config = popupConfig.reactionPopupConfig;
    }

    public void StartRelactionPopup(SinglePersonConsequences consequences)
    {
        if (isPopupActive)
        {
            popup.ShowReaction(DefineText(consequences));
        }
        else
        {
            BeginPopup(consequences);
        }
    }

    private void BeginPopup(SinglePersonConsequences consequences)
    {
        isPopupActive = true;
        popup = popupManager.ShowPopup(PopupType.ReactionPopup, false) as ReactionPopup;
        popup.Init(factory, config);
        popup.ShowReaction(DefineText(consequences));
        popup.OnReactionFinished += FinsihPopup;
    }

    private string DefineText(SinglePersonConsequences consequences)
    {
        string firstHalf = consequences.PersonName.Name;
        string secondHalf = consequences.Value > 0 ? "одобряет это" : "не одобряeт это";
        return $"{firstHalf} {secondHalf}";
    }
    private void FinsihPopup()
    {
        popup.OnReactionFinished -= FinsihPopup;
        popupManager.ClosePopup(PopupType.ReactionPopup);
        isPopupActive = false;
    }

}


