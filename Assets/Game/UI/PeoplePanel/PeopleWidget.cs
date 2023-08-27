using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PeopleWidget : MonoBehaviour
{
    [SerializeField]
    private Text peopleText;

    [SerializeField] private PeopleHighlightComponent peopleHighlightComponent;

    public void SetPeople(string value)
    {
        var oldValue = peopleText.text; //crutch for escape Highlight when scene loaded
        if (peopleText != null)
        {
            peopleText.text = $"{value}";
        }

        if (string.IsNullOrEmpty(oldValue))
            return;

        peopleHighlightComponent.Highlight();
    }
}
