using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageParamView : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private GameObject panel;

    public void SetParam(int value)
    {
        string textValue = value > 0 ? $"+{value}" : value.ToString();
        uiText.text = textValue;
        SetValueColor(uiText, value);
        panel.SetActive(true);
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
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
