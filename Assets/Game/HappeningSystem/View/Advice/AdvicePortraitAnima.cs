using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AdvicePortraitAnima : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private List<Image> images;
    [SerializeField] private float horizonOffset;
    [SerializeField] private float verticalOverOffset;
    [SerializeField] private float verticalClickOffset = 0.04f;
    [SerializeField] private bool isChangeColor = true;
    private bool isBlock;
    private void Start()
    {
        SetColor(0.95f);
        SetMode(isBlock);
    }

    //ui this - portait view
    public void SetState(bool value)
    {
        isBlock = value;
    }

    private void SetMode(bool value)
    {
        if (TryGetComponent<Button>(out Button button))
            button.interactable = !value;
        var color = value ? 0.6f : 0.95f;
        SetColor(color);
    }

    
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (isBlock) return;
        SetColor(0.75f);
        transform.position = new Vector3(transform.position.x, transform.position.y - verticalClickOffset, transform.position.z);
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (isBlock) return;
        SetColor(0.95f);
        transform.position = new Vector3(transform.position.x, transform.position.y + verticalClickOffset, transform.position.z);
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (isBlock) return;
        transform.localPosition = new Vector3(transform.localPosition.x + horizonOffset, transform.localPosition.y + verticalOverOffset, transform.localPosition.z);
        SetColor(1f);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (isBlock) return;
        transform.localPosition = new Vector3(transform.localPosition.x - horizonOffset, transform.localPosition.y - verticalOverOffset, transform.localPosition.z);
        SetColor(0.95f);
    }


    private void SetColor(float value)
    {
        if (!isChangeColor) return;
        foreach (var image in images)
        {
            image.color = new Color(value, value, value, 1);
        }
    }
}
