using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Events;

public class CustomDialoguePosition : MonoBehaviour
{
    private static CustomDialoguePosition m_instance = null;
    public static UnityEvent<bool> IsFlipped = new();
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
        if (m_instance != null && m_instance.transform != null)
        {
            if (m_instance.transform.childCount < index)
                index = m_instance.transform.childCount - 1;

            Vector3 position = m_instance.transform.GetChild(index).position;

            if (index % 2 != 0)
                position = new Vector3(position.x, position.y, -5);
            else
                position = new Vector3(position.x, position.y, -7);
        
            return position;
        }

        return new Vector3(0, 3, -10);
    }

    public static bool GetFlippedState()
    {
        return MathF.Abs(m_instance.transform.rotation.z) > 0.5f;
    }

    public static void Flip(float angle)
    {
        m_instance.transform.rotation = new Quaternion(m_instance.transform.rotation.x, m_instance.transform.rotation.y, angle, m_instance.transform.rotation.w);
        IsFlipped.Invoke(GetFlippedState());
    }
}
