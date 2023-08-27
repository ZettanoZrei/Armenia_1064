using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageRelationView : MonoBehaviour
{
    [SerializeField] private Text uiName;
    [SerializeField] private Text uiValue;

    public void SetRelation(string name, int value)
    {
        uiName.text = name;
        uiValue.text = value > 0 ? $"+{value}" : value.ToString();
        SetValueColor(uiValue, value);
    }

    private void SetValueColor(Text uiValue, int value)
    {
        if (value > 0)
        {
            uiValue.color = Color.green;
        }
        else if (value < 0)
        {
            uiValue.color = Color.red;
        }
    }
}
