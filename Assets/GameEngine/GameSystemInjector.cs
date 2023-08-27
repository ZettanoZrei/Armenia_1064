using GameSystems;
using UnityEngine;
using Zenject;

public class GameSystemInjector : MonoBehaviour
{
    private GameSystem gameSystem;
    [Inject] private MySceneManager sceneManager;

    private void Awake()
    {
        gameSystem = FindObjectOfType<GameSystem>();
        sceneManager.InitGameSystem(gameSystem);
    }
}
