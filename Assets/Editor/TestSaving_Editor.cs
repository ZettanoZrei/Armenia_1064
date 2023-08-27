using UnityEngine;
using UnityEditor;
using Assets.GameEngine.TEST_SYSTEMS;

[CustomEditor(typeof(TEST_SAVE_SYSTEM))]
class TestSaving_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var tagetScript = target as TEST_SAVE_SYSTEM;

        if(GUILayout.Button("DO SAVE TEST"))
        {
            tagetScript.DoTest();
        }
        EditorGUILayout.LabelField(tagetScript.Result.ToString());
    }
}
