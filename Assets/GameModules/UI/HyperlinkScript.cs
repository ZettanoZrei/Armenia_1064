using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HyperlinkScript : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string uri;
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Application.OpenURL(uri);
    }
}
