using Assets.Modules.UI;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class RelationPanelView : MonoBehaviour
{
    [SerializeField] private SimpleButton hideButton;
    [SerializeField] private SimpleButton showButton;

    [SerializeField] private float offet = 1.35f;
    [SerializeField] private float time = 0.5f;

    [SerializeField] private List<PortraitShell> portraitShells = new List<PortraitShell>();

    private void OnEnable()
    {
        hideButton.OnClick += HidePanelHandler;
        showButton.OnClick += ShowPanelHandler;
    }

    private void OnDisable()
    {
        hideButton.OnClick -= HidePanelHandler;
        showButton.OnClick -= ShowPanelHandler;
    }


    public void SetLevel(string name, int value)
    {
        var portraitComponent = portraitShells.FirstOrDefault(x => x.name == name);
        if (portraitComponent == null)
            return;

        portraitComponent.portraitView.SetRelationLevel(value);
    }

    private void ShowPanelHandler()
    {
        hideButton.gameObject.SetActive(true);
        showButton.gameObject.SetActive(false);
        transform.DOMoveY(transform.position.y + offet, time);
    }

    private void HidePanelHandler()
    {
        hideButton.gameObject.SetActive(false);
        showButton.gameObject.SetActive(true);
        transform.DOMoveY(transform.position.y - offet, time);
    }

    [Serializable]
    class PortraitShell
    {
        public PortraitView portraitView;
        public string name;
    }
}
