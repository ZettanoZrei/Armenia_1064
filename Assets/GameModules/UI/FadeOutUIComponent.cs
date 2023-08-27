using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GameModules.UI
{
    internal class FadeOutUIComponent : MonoBehaviour
    {
        public event Action OnFaded;
        public event Action OnAppeared;
        public void FadeOut(float duration, params MaskableGraphic[] uiElements)
        {
            if (!uiElements.Any())
            {
                return;
            }
            if (this != null)
                this.StartCoroutine(FadeOutCoroutine(duration, uiElements));
        }

        public void Appear(float duration, params MaskableGraphic[] uiElements)
        {
            if (!uiElements.Any())
            {
                return;
            }
            StartCoroutine(AppearCoroutine(duration, uiElements));
        }

        private IEnumerator AppearCoroutine(float duration, params MaskableGraphic[] uiElements)
        {
            float elapsedTime = 0f;
            Color startColor = uiElements[0].color;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
                foreach (var uiElement in uiElements)
                    uiElement.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                yield return null;
            }

            foreach (var uiElement in uiElements)
                uiElement.color = new Color(startColor.r, startColor.g, startColor.b, 1);

            OnAppeared?.Invoke();
        }


        private IEnumerator FadeOutCoroutine(float duration, params MaskableGraphic[] uiElements)
        {
            float elapsedTime = 0f;
            Color startColor = uiElements[0].color;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
                foreach (var uiElement in uiElements)
                    uiElement.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                yield return null;
            }

            foreach (var uiElement in uiElements)
                uiElement.color = new Color(startColor.r, startColor.g, startColor.b, 0);

            OnFaded?.Invoke();
        }
    }
}
