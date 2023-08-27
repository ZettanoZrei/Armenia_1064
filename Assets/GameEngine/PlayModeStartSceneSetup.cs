using System.Collections;
using System.Collections.Generic;
using UnityEditor;



#if UNITY_EDITOR

//Просто кидаете файл с кодом в любую папку, и 
//он будет автоматически срабатывать при запуске редактора
//или изменении списка сцен в настройках
[InitializeOnLoad]
public class PlayModeStartSceneSetup
{
    public const int START_SCENE_INDEX = 0;

    static PlayModeStartSceneSetup()
    {
        SceneListChanged();
        EditorBuildSettings.sceneListChanged += SceneListChanged;
    }

    static void SceneListChanged()
    {
        if (EditorBuildSettings.scenes.Length == 0) return;
        SceneAsset scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[START_SCENE_INDEX].path);
        UnityEditor.SceneManagement.EditorSceneManager.playModeStartScene = scene;
    }
}

#endif