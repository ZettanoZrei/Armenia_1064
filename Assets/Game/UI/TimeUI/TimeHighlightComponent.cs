using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.UI.TimeUI
{
    class TimeHighlightComponent : MonoBehaviour
    {
        [SerializeField] private Sprite lightBase;
        [SerializeField] private Sprite lightProgress;
        [SerializeField] private Sprite _base;
        [SerializeField] private Sprite progress;


        [SerializeField] private Image baseImage;
        [SerializeField] private Image progressImage;
        public void Highlight()
        {
            if(this.gameObject.activeSelf)
                StartCoroutine(Coroutine());
        }

        IEnumerator Coroutine()
        {
            baseImage.sprite = lightBase;
            progressImage.sprite = lightProgress;
            yield return new WaitForSecondsRealtime(0.2f);
            baseImage.sprite = _base;
            progressImage.sprite = progress;
        }
    }
}
