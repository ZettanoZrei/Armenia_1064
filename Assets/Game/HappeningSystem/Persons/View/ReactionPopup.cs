using Assets.Game;
using Assets.Game.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


class ReactionPopup : Popup
{
    private readonly List<ReactionPart> reactionPopupParts = new List<ReactionPart>();
    private ReactionPart.Factory factory;
    private PopupConfig.ReactionPopupConfig config;
    private CompositeDisposable disposables;
    public event Action OnReactionFinished;
    int count = -1;


    private void OnDestroy()
    {
        OnReactionFinished?.Invoke();
    }
    public void Init(ReactionPart.Factory factory, PopupConfig.ReactionPopupConfig config)
    {
        this.factory = factory;
        this.config = config;
    }
    public void ShowReaction(string text)
    {
        count++;
        Observable.Timer(TimeSpan.FromSeconds(config.appearSpeed * count))
                .Subscribe(_ => DoShow(text))
                .AddTo(disposables);


        void DoShow(string text)
        {
            ReactionPart reaction = CreateReaction(text);
            reaction.TransformNullify();
            this.reactionPopupParts.Add(reaction);
            SetReactionDurationTimer(reaction);
        }
    }

    private ReactionPart CreateReaction(string text)
    {
        var reaction = this.factory.Create(text);
        reaction.transform.SetParent(this.transform);
        
        return reaction;
    }

    private void SetReactionDurationTimer(ReactionPart reaction)
    {
        Observable.Timer(TimeSpan.FromSeconds(config.stayTime))
            .Subscribe(_ => ReactionTimeRunOut(reaction))
            .AddTo(disposables);
    }

    private void ReactionTimeRunOut(ReactionPart reaction)
    {
        reaction.Dispose();
        reactionPopupParts.Remove(reaction);

        if (!reactionPopupParts.Any())
        {
            count = -1;
            OnReactionFinished?.Invoke();
        }
    }

    void OnEnable()
    { 
        // создаем disposable
        disposables = new CompositeDisposable();
    }

    void OnDisable()
    { // уничтожаем подписки
        if (disposables != null)
        {
            disposables.Dispose();
        }
    }
}


