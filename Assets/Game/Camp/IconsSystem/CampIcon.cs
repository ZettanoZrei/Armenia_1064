using Model.Entities.Persons;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CampIcon : PortraitView
{
    private bool isRequired;
    private string quest;
    [SerializeField] private Image exclamation;

    public event Action<CampIcon> OnClose;
    public bool IsRequired => isRequired;

    public event Action<string> OnIconClick;
    [Inject]
    public void Construct(bool isRequired, string quest)
    {
        this.isRequired = isRequired;
        this.quest = quest;
    }
    public Vector2 GetSize()
    {
        var rectTransform = gameObject.GetComponent<RectTransform>();
        return rectTransform.sizeDelta;
    }
    private void OnEnable()
    {
        exclamation.gameObject.SetActive(isRequired);
    }

    private void OnDisable()
    {
        OnClose?.Invoke(this);
    }
    //ui this
    public void InvokeClickAction()
    {
        OnIconClick?.Invoke(quest);
    }
    public class Factory : PlaceholderFactory<bool, string, CampIcon>
    {
    }
}
