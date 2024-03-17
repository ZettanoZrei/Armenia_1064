using Assets.Modules;
using GameSystems.Modules;
using Model.Entities.Persons;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using wrappers = PixelCrushers.DialogueSystem.Wrappers;


/// <summary>
/// Pixel Crushers DialogsController to launch a chain of dialogs (диалоги идущие друг за другом без перерыва)
/// </summary>
public class DialogsCnainController : MonoBehaviour,
    IInitializable,
    ISceneReady,
    ISceneFinish,
    IGameLeave
{
    private wrappers.DialogueSystemController dialogueSystemController;
    private SignalBus signalBus;
    private Transform conversationActor;
    private Transform storyActor;

    private string title;
    private Transform currentActor;


    [Inject]
    public void Construct(wrappers.DialogueSystemController dialogueSystemController, SignalBus signalBus,
        [Inject(Id = "conversationActor")] Transform conversationActor, [Inject(Id = "storyActor")] Transform storyActor)
    {
        this.dialogueSystemController = dialogueSystemController;
        this.signalBus = signalBus;
        this.conversationActor = conversationActor;
        this.storyActor = storyActor;
    }

    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });

    }
    void ISceneReady.ReadyScene()
    {
        Lua.RegisterFunction(nameof(StartDialog), this, SymbolExtensions.GetMethodInfo(() => StartDialog(String.Empty, default(Actor))));
    }

    void ISceneFinish.FinishScene()
    {
        Lua.UnregisterFunction(nameof(StartDialog));
        Nullify();
    }
    void IGameLeave.LeaveGame()
    {
        Nullify();
    }

    public void StartDialog(string title, Actor actor)
    {
        this.title = title;
        switch (actor.Name)
        {
            case "Conversation":
                currentActor = conversationActor;
                break;
            case "Story":
                currentActor = storyActor;
                break;
            default: throw new Exception($"Unknown actor: {actor.Name}");
        }
    }

    //starts by unity event after dialog gas finished 
    public async void StartDialog()
    {
        if (string.IsNullOrEmpty(title) || currentActor is null) return;
        await Task.Delay(TimeSpan.FromSeconds(0.1)); //todo if without delay then ui will not be showed
        DialogueManager.StartConversation(title, currentActor);
        Nullify();
    }

    private void Nullify()
    {
        title = null;
        currentActor = null;
    }
}


