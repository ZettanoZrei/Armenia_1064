using Newtonsoft.Json.Bson;
using UnityEngine;
using UnityEngine.UI;


public sealed class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image progress;

    [SerializeField]
    private GameObject root;

    public float Value => progress.fillAmount;

    private Sprite cacheSprite;
    private void Awake()
    {
        cacheSprite = progress.sprite;
    }

    public void SetProgress(float progress)
    {
        this.progress.fillAmount = Mathf.Clamp01(progress);
    }

    public void SetSprite(Sprite sprite)
    {
        progress.sprite = sprite;
    }

    public void SetDefault()
    {
        progress.sprite = cacheSprite;
    }
}
