using UnityEngine;
using UnityEditor;
using Assets.GameEngine.TEST_SYSTEMS;
using Zenject;
using Assets.Game.Configurations;

[CustomEditor(typeof(GameSettingsInstaller))]
class GameSettingsInstaller_Editor : Editor
{
    [Inject] public GameSettingsInstaller config;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var tagetScript = target as GameSettingsInstaller;
        if (GUILayout.Button("Release State"))
        {
            tagetScript.SetReleaseState();
        }
        if (GUILayout.Button("Debug State"))
        {
            tagetScript.SetDebugState();
        }
    }
}