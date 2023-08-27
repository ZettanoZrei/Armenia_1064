using Model.Entities.Persons;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class PortraitView : MonoBehaviour
{
    [SerializeField] private Image portrait;
    [SerializeField] private List<GameObject> relationLevels;
    [SerializeField] protected GameObject relationPanel;
    [SerializeField] private PortraitName portraitName;
    protected PersonName personName;
    private int relationRestriction = 3;

    [SerializeField] private UnityEvent<bool> OnSetMode;

    public PersonName PersonName => personName;

    public void SetDialogPortrait(Sprite sprite, bool mainCharacter, PersonName personName = null, int relationValue = 0)
    {
        this.portrait.sprite = sprite;
        this.personName = personName;
        if (personName != null && portraitName != null)
            portraitName.SetPortraitName(personName.Name);
        SetRelationLevel(relationValue);

        relationPanel.SetActive(mainCharacter);
    }

    public void SetPortrait(Sprite sprite, PersonName personName = null, int relationValue = 0, int relationRestriction = 3)
    {
        this.relationRestriction = relationRestriction;
        this.portrait.sprite = sprite;
        this.personName = personName;
        if (personName != null && portraitName != null)
            portraitName.SetPortraitName(personName.Name);
        SetRelationLevel(relationValue);
        SetState(relationValue);
    }
    public void SetTransformSettings(Vector3 vector)
    {
        this.transform.localPosition = vector;
        this.transform.localScale = Vector3.one;
        this.transform.localRotation = Quaternion.identity; 
    }
    public void SetRelationLevel(int value)
    {
        for (var i = 0; i < relationLevels.Count; i++)
        {
            if (value >= i + 1)
                relationLevels[i].SetActive(true);
            else
                relationLevels[i].SetActive(false);
        }
    }

    private void SetState(int relationValue)
    {
        var value = relationValue < relationRestriction;
        OnSetMode?.Invoke(value);
    }
}


