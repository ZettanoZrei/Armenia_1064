using Assets.Modules;
using Assets.Save;
using Entities;
using GameSystems.Modules;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CaravanRepositoryBSController : IInitializable,
    IGameInitElement, 
    IGameStartElement
{
    private ISaveComponent<List<CaravanData>> saveComponent;
    private BSRepositoryCaravan repositoryBS;
    private IEntity caravan;
    private MySceneManager sceneManager;
    private readonly SignalBus signalBus;

    public CaravanRepositoryBSController([Inject(Id = "caravan")] IEntity caravan, BSRepositoryCaravan repositoryCaravan, MySceneManager sceneManager,
        SignalBus signalBus)
    {
        this.caravan = caravan;
        this.repositoryBS = repositoryCaravan;
        this.sceneManager = sceneManager;
        this.signalBus = signalBus;
    }

    void IInitializable.Initialize()
    {
        signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
    }
    void IGameInitElement.InitGame()
    {
        saveComponent = this.caravan.Element<ISaveComponent<List<CaravanData>>>();
    }

    void IGameStartElement.StartGame()
    {
        Load();
    }
    private void Update()
    {
        Save();
    }

    private void Save()
    {
        var data = saveComponent.GetData();
        repositoryBS.Add(sceneManager.CurrentScene, data);
    }

    private void Load()
    {
        if (repositoryBS.TryGetData(sceneManager.CurrentScene, out var data))
        {
            saveComponent.LoadData(data);
        }
    }   
}

