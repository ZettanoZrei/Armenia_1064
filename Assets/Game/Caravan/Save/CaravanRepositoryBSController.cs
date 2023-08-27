using Assets.Save;
using Entities;
using GameSystems;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CaravanRepositoryBSController : MonoBehaviour,
    IGameInitElement, IGameStartElement
{
    private ISaveComponent<List<CaravanData>> saveComponent;
    private BSRepositoryCaravan repositoryBS;
    private IEntity caravan;
    private MySceneManager sceneManager;

    [Inject]
    public void Construct([Inject(Id = "caravan")] IEntity caravan, BSRepositoryCaravan repositoryCaravan, MySceneManager sceneManager)
    {
        this.caravan = caravan;
        this.repositoryBS = repositoryCaravan;
        this.sceneManager = sceneManager;
    }
    void IGameInitElement.InitGame(IGameSystem _)
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

