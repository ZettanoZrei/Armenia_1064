using Assets.DialogSystem.Scripts;
using Assets.Game.HappeningSystem.Persons;
using Assets.Game.Parameters;
using Assets.Modules;
using GameSystems.Modules;
using PixelCrushers.DialogueSystem;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class DialogConclusionManager :
    IInitializable,
    ISceneReady,
    ISceneFinish
{
    private DialogConclusion dialogConclusion;
    private RelationManager relationManager;
    private ParametersManager parametersManager;
    private SignalBus signalBus;
    private DialogConclusionAgent conclusionAgent;


    [Inject]
    public void Construct(ParametersManager parametersManager, RelationManager relationManager, SignalBus signalBus, DialogConclusionAgent conclusionAgent)
    {
        this.parametersManager = parametersManager;
        this.relationManager = relationManager;
        this.signalBus = signalBus;
        this.conclusionAgent = conclusionAgent;
    }

    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
    }

    void ISceneReady.ReadyScene()
    {
        Lua.RegisterFunction(nameof(AddItems), this, SymbolExtensions.GetMethodInfo(() => AddItems(default(int), string.Empty)));
        Lua.RegisterFunction(nameof(SetRelationship), this, SymbolExtensions.GetMethodInfo(() => SetRelationship(string.Empty, default(int))));
        Lua.RegisterFunction(nameof(LaunchConclusion), this, SymbolExtensions.GetMethodInfo(() => LaunchConclusion()));
    }

    void ISceneFinish.FinishScene()
    {
        Lua.UnregisterFunction(nameof(AddItems));
        Lua.UnregisterFunction(nameof(SetRelationship));
        Lua.UnregisterFunction(nameof(LaunchConclusion));
    }
    public void LaunchConclusion()
    {
        conclusionAgent.LaunchConclusion(dialogConclusion);
    }
    public void AddItems(double value, string item)
    {
        if (dialogConclusion == null) return;

        switch (item)
        {
            case "Food":
                dialogConclusion.food += (int)value;
                break;
            case "Stamina":
                dialogConclusion.stamina += (int)value;
                break;
            case "Blessing":
                dialogConclusion.blessing += (int)value;
                break;
            case "People":
                dialogConclusion.people += (int)value;
                break;
            default: throw new Exception("Unknown item");
        }

    }

    public void SetRelationship(string actor, double value)
    {
        if (dialogConclusion == null) return;
        if (dialogConclusion.persons.Any(x => x.actor == actor))
        {
            var person = dialogConclusion.persons.First(x => x.actor == actor);
            person.relations.Add((int)value);
        }
        else
        {
            var person = new DialogConclusion.PersonResult { actor = actor };
            person.relations.Add((int)value);
            dialogConclusion.persons.Add(person);
        }
    }

    //Один dialogConclusion может использоваться на несколько диалогов
    public void CreateDialogConclusion()
    {
        if (dialogConclusion != null) return;
        dialogConclusion = new DialogConclusion();
    }


    public void ExecuteResults()
    {
        if (dialogConclusion == null) return;

        parametersManager.ChangeFood(dialogConclusion.food);
        parametersManager.ChangeStamina(dialogConclusion.stamina);
        parametersManager.ChangePeople(dialogConclusion.people);

        foreach (var person in dialogConclusion.persons)
        {
            var value = person.relations.Sum();
            relationManager.ChangeRelation(person.actor, value);
        }

        dialogConclusion = null;
    }
}


