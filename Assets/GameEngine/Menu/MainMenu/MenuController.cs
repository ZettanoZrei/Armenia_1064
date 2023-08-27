using Assets.Modules.UI;
using Assets.Systems.SaveSystem;
using GameSystems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MenuController : MonoBehaviour, 
    IGameReadyElement, IGameStartElement, IGameFinishElement
{
    [SerializeField] private SimpleButton _continue;
    [SerializeField] private SimpleButton newGame;
    [SerializeField] private SimpleButton settings;
    [SerializeField] private SimpleButton exit;
    [SerializeField] private SimpleButton encyclopedia;

    private SaveHelper<SaveData> saveHelper;
    private IMenuCommand continueCommand;
    private IMenuCommand startCommand;
    private IMenuCommand settingsCommand;

    [Inject]
    public void Construct(List<IMenuCommand> menuCommands, SaveHelper<SaveData> saveHelper)
    {
        this.saveHelper = saveHelper;
        continueCommand = menuCommands.First(x => x is ContinueCommand);
        startCommand = menuCommands.First(x => x is NewGameCommand);
        settingsCommand = menuCommands.First(x => x is SettingsCommand);
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
