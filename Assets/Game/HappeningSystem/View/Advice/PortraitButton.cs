using Model.Entities.Persons;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
public class PortraitButton : PortraitView
{
    public event Action<PersonName> OnPortraitClick;

    public void SetAcivate(bool value)
    {
        if (value is true)
        {
            if (this.gameObject.activeSelf)
                return;
            this.gameObject.SetActive(true);
        }
        else
        {
            if (!this.gameObject.activeSelf)
                return;
            this.gameObject.SetActive(false);
        }
    }

    //ui this
    public void InvokeClickAction()
    {
        OnPortraitClick?.Invoke(personName);
    }



    public class Factory : PlaceholderFactory<PortraitButton>
    {
    }
}
