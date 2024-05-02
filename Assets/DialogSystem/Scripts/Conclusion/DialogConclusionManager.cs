using Assets.DialogSystem.Scripts;
using Assets.Game.HappeningSystem.Persons;
using Assets.Game.Parameters;
using Assets.Modules;
using GameSystems.Modules;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.ChatMapper;
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
        Lua.RegisterFunction(nameof(AddItems), this, SymbolExtensions.GetMethodInfo(() => AddItems(0, string.Empty)));
        //Lua.RegisterFunction(nameof(SetRelationship), this, SymbolExtensions.GetMethodInfo(() => SetRelationship(0, string.Empty)));
        Lua.RegisterFunction(nameof(LaunchConclusion), this, SymbolExtensions.GetMethodInfo(() => LaunchConclusion()));
        Lua.RegisterFunction(nameof(ChangeRelationship), this, SymbolExtensions.GetMethodInfo(() => ChangeRelationship(string.Empty, 0)));
    }

    void ISceneFinish.FinishScene()
    {
        Lua.UnregisterFunction(nameof(AddItems));
        //Lua.UnregisterFunction(nameof(SetRelationship));
        Lua.UnregisterFunction(nameof(LaunchConclusion));
        Lua.UnregisterFunction(nameof(ChangeRelationship));
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


    public void ChangeRelationship(string name, double value)
    {
        Debug.Log(name);
        if (dialogConclusion == null) return;
        if (dialogConclusion.persons.Any(x => x.actor == name))
        {
            var person = dialogConclusion.persons.First(x => x.actor == name);
            person.relations.Add((int)value);
        }
        else
        {
            var person = new DialogConclusion.PersonResult { actor = name };
            person.relations.Add((int)value);
            dialogConclusion.persons.Add(person);
        }
    }

    //public void SetRelationship(double value, string actor)
    //{
    //    if (dialogConclusion == null) return;
    //    if (dialogConclusion.persons.Any(x => x.actor == actor))
    //    {
    //        var person = dialogConclusion.persons.First(x => x.actor == actor);
    //        person.relations.Add((int)value);
    //    }
    //    else
    //    {
    //        var person = new DialogConclusion.PersonResult { actor = actor };
    //        person.relations.Add((int)value);
    //        dialogConclusion.persons.Add(person);
    //    }
    //}

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


