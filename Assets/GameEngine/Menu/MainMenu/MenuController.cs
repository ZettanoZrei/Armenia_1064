using Assets.Modules;
using Assets.Modules.UI;
using Assets.Systems.SaveSystem;
using GameSystems.Modules;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MenuController : IInitializable,
    IGameReadyElement,
    IGameStartElement,
    IGameFinishElement
{
    private SimpleButton _continue;
    private SimpleButton newGame;
    private SimpleButton settings;
    private SimpleButton exit;
    private SimpleButton encyclopedia;

    private SaveHelper<SaveData> saveHelper;
    private readonly SignalBus signalBus;
    private IMenuCommand continueCommand;
    private IMenuCommand startCommand;
    private IMenuCommand settingsCommand;

    public MenuController(List<IMenuCommand> menuCommands, SaveHelper<SaveData> saveHelper, SignalBus signalBus,
        [Inject(Id = "continue")] SimpleButton _continue, [Inject(Id = "newGame")] SimpleButton newGame, [Inject(Id = "settings")] SimpleButton settings,
        [Inject(Id = "exit")] SimpleButton exit, [Inject(Id = "encyclopedia")] SimpleButton encyclopedia)
    {
        this._continue = _continue;
        this.newGame = newGame;
        this.settings = settings;
        this.exit = exit;
        this.encyclopedia = encyclopedia;
        this.saveHelper = saveHelper;
        this.signalBus = signalBus;
        continueCommand = menuCommands.First(x => x is ContinueCommand);
        startCommand = menuCommands.First(x => x is NewGameCommand);
        settingsCommand = menuCommands.First(x => x is SettingsCommand);
    }

    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
    }
    void IGameReadyElement.ReadyGame()
    {
        _continue.OnClick += ContinueHandler;
        newGame.OnClick += StartHandler;
        settings.OnClick += SettingsHandler;
        exit.OnClick += ExitHandler;
    }
    void IGameStartElement.StartGame()
    {
        _continue.SetActiveButton(saveHelper.CheckSaveFile());
        encyclopedia.SetActiveButton(false);
    }

    void IGameFinishElement.FinishGame()
    {
        _continue.OnClick -= ContinueHandler;
        newGame.OnClick -= StartHandler;
        settings.OnClick -= SettingsHandler;
        exit.OnClick -= ExitHandler;
    }
    private void StartHandler()
    {
        startCommand.Execute();
    }

    private void ContinueHandler()
    {
        continueCommand.Execute();
    }

    private void SettingsHandler()
    {
        settingsCommand.Execute();
    }

    private void ExitHandler()
    {
        Application.Quit();
    }
}
