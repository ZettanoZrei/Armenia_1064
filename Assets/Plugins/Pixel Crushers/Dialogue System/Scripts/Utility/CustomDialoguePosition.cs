using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDialoguePosition : MonoBehaviour
{
    private static CustomDialoguePosition m_instance = null;

    private void Awake()
    {
        m_instance = this;
    }
    private void OnDestroy()
    {
        m_instance = null;
    }

    public static Vector3 GetPosition(int index)
    {
        if (m_instance.transform.childCount < index)
            index = m_instance.transform.childCount - 1;

        Vector3 position = m_instance.transform.GetChild(index).position;

        if (index % 2 != 0)
            position = new Vector3(position.x, position.y, -10);
        else
            position = new Vector3(position.x, position.y, -1);
        
        return position;
    }
}
