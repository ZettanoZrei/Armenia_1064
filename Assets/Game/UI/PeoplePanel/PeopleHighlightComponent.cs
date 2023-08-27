using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class PeopleHighlightComponent : MonoBehaviour
{
    [SerializeField] private new Sprite light;
    [SerializeField] private Sprite normal;


    [SerializeField] private Image image;
    public void Highlight()
    {
        if (this.gameObject.activeSelf)
            StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        image.sprite = light;
        yield return new WaitForSecondsRealtime(0.2f);
        image.sprite = normal;
    }
}
