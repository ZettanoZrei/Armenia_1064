using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.GameEngine.TEST_SYSTEMS;

public class Menu_Functions
{
    [MenuItem("Tools/Runtime_TestSaving")]
    public static void TestSaving()
    {
        GameObject.FindObjectOfType<TEST_SAVE_SYSTEM>().DoTest();
    }

    [MenuItem("Tools/Runtime_ManualSave")]
    public static void ManualSaving()
    {
        GameObject.FindObjectOfType<TEST_SAVE_SYSTEM>().Save();
    }
}
