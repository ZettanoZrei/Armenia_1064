using Assets.Game.HappeningSystem.View.Advice;
using Assets.Modules.UI;
using Model.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public class AdvicePopupView : MonoBehaviour
{
    private List<PortraitButton> portraitButtons = new List<PortraitButton>();

    [SerializeField]
    private List<AdvicePortraitContainer> advicePortraitContainers = new List<AdvicePortraitContainer>();

    [SerializeField] private SimpleButton up;
    [SerializeField] private SimpleButton down;
    private PortaitHeap portraitHeap;

    private int minShift = 0, maxShift = 0, shift = 0;

    public event Action<PersonName> OnClickPortrait;



    public void ShowAdvicePanel(List<PortraitButton> portraits) //delete?
    {
        try
        {
            CleanPastPortraits();
            this.portraitButtons = portraits;
            ContainerSwitchOnOff(portraits.Count);
            NavigateButtonSwitchOnOff(portraits.Count);
            UpdatePortraits();
            portraits.ForEach(x => x.OnPortraitClick += this.InvokeClick);
            maxShift = Math.Max(portraitButtons.Count - advicePortraitContainers.Count, 0);
            this.gameObject.SetActive(true);
        }
        catch(Exception ex) { Logger.WriteLog($"{ex}"); }
    }


    public void ActiveAdvicePanel()
    {
        this.gameObject.SetActive(true);
    }
    public void HideAdvicePanel()
    {
        this.gameObject.SetActive(false);
    }

    public void InitPortraitHeap(PortaitHeap portraitHeap)
    {
        this.portraitHeap = portraitHeap;
    }

    private void ContainerSwitchOnOff(int portraitCount)
    {
        foreach (var container in advicePortraitContainers)
        {
            if (container.Number < portraitCount)
                container.SetActive(true);
            else
                container.SetActive(false);
        }
    }

    private void NavigateButtonSwitchOnOff(int portraitCount)
    {
        if (portraitCount < advicePortraitContainers.Count)
        {
            up.SetActiveObject(false);
            down.SetActiveObject(false);
        }
        else
        {
            up.SetActiveObject(true);
            down.SetActiveObject(true);
        }
    }
    private void CleanPastPortraits()
    {
        portraitButtons.ForEach(x => x.OnPortraitClick -= this.InvokeClick);
        while (portraitButtons.Count > 0)
        {
            portraitButtons.RemoveAt(0);
        }
    }
    private void Up()
    {
        shift = Math.Max(shift - 1, minShift);
        UpdatePortraits();
    }

    private void Down()
    {
        shift = Math.Min(shift + 1, maxShift);
        UpdatePortraits();
    }

    private void UpdatePortraits()
    {
        for (var k = 0; k < portraitButtons.Count; k++)
        {
            var number = k - shift;
            var container = advicePortraitContainers.FirstOrDefault(x => x.Number == number);
            if (container != null)
            {
                container.SetPortrait(portraitButtons[k]);
            }
            else
            {
                PortraitSwitchOff(portraitButtons[k]);               
            }
        }
    }
    private void PortraitSwitchOff(PortraitButton portrait)
    {
        Logger.WriteLog($"{portraitHeap}");
        portrait.transform.SetParent(portraitHeap.transform);
        portrait.SetAcivate(false);
    }

    private void InvokeClick(PersonName name)
    {
        this.OnClickPortrait?.Invoke(name);
    }
    private void OnEnable()
    {
        up.OnClick += Up;
        down.OnClick += Down;
    }

    private void OnDisable()
    {
        up.OnClick -= Up;
        down.OnClick -= Down;
    }
}
