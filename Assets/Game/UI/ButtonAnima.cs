using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnima : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite usualSprite;
    [SerializeField] private Sprite overSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite inactiveSprite;

    private Image spriteRenderer;
    private const float offset = 0.04f;
    private bool isActive = true;
    private void Awake()
    {
        spriteRenderer = GetComponent<Image>();
        spriteRenderer.sprite = usualSprite;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (!isActive) return;
        spriteRenderer.sprite = downSprite;
        transform.position = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (!isActive) return;
        spriteRenderer.sprite = usualSprite;
        transform.position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (!isActive) return;
        spriteRenderer.sprite = overSprite;
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!isActive) return;
        spriteRenderer.sprite = usualSprite;
    }

    public void SetActive(bool value)
    {
        isActive = value;

        if (!isActive && inactiveSprite != null)
            spriteRenderer.sprite = inactiveSprite;
    }
}
