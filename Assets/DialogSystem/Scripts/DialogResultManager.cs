using Assets.Game.HappeningSystem.Persons;
using Assets.Game.Parameters;
using Assets.Modules;
using GameSystems.Modules;
using PixelCrushers.DialogueSystem;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

class DialogResultManager : MonoBehaviour,
    IInitializable,
    ISceneReady,
    ISceneFinish
{
    [SerializeField] private bool immediatelyResult;
    [SerializeField] private DialogResult dialogResultPerfab;
    private DialogResult dialogResult;
    private RelationManager relationManager;
    private ParametersManager parametersManager;
    private SignalBus signalBus;

    [Inject]
    public void Construct(ParametersManager parametersManager, RelationManager relationManager, SignalBus signalBus)
    {
        this.parametersManager = parametersManager;
        this.relationManager = relationManager;
        this.signalBus = signalBus;
    }

    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
    }

    void ISceneReady.ReadyScene()
    {
        Lua.RegisterFunction(nameof(AddItems), this, SymbolExtensions.GetMethodInfo(() => AddItems(default(int), default(Item))));
        Lua.RegisterFunction(nameof(SetRelationship), this, SymbolExtensions.GetMethodInfo(() => SetRelationship(default(Actor), default(int))));
    }

    void ISceneFinish.FinishScene()
    {
        Lua.UnregisterFunction(nameof(AddItems));
        Lua.UnregisterFunction(nameof(SetRelationship));
    }

    public void AddItems(double value, Item item)
    {
        if (dialogResult == null) return;

        switch (item.Name)
        {
            case "Food":
                dialogResult.food += (int)value;
                break;
            case "Stamina":
                dialogResult.stamina += (int)value;
                break;
            case "Blessing":
                dialogResult.blessing += (int)value;
                break;
            case "People":
                dialogResult.people += (int)value;
                break;
            default: throw new Exception("Unknow item");
        }
        if (immediatelyResult) ExecuteResults();
    }

    public void SetRelationship(Actor actor, double value)
    {
        if (dialogResult == null) return;
        if (dialogResult.persons.Any(x => x.actor.Name == actor.Name))
        {
            var person = dialogResult.persons.First(x => x.actor.Name == actor.Name);
            person.relations.Add((int)value);
        }
        else
        {
            var person = new DialogResult.PersonResult { actor = actor };
            person.relations.Add((int)value);
            dialogResult.persons.Add(person);
        }

        if (immediatelyResult) ExecuteResults();
    }

    public void CreateDialogResult()
    {
        dialogResult = MonoBehaviour.Instantiate(dialogResultPerfab);
    }

    public void DestroyDialogResult()
    {
        if (dialogResult != null)
            MonoBehaviour.Destroy(dialogResult);
    }

    public void ExecuteResults()
    {
        if (dialogResult == null) return;

        parametersManager.ChangeFood(dialogResult.food);
        parametersManager.ChangeStamina(dialogResult.stamina);
        parametersManager.ChangePeople(dialogResult.people);

        foreach (var person in dialogResult.persons)
        {
            var value = person.relations.Sum();
            relationManager.ChangeRelation(person.actor.Name, value);
        }

        dialogResult.Nullify();
    }
}


