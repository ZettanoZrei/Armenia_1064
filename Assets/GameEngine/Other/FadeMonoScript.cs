using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GameEngine.Other
{
    internal class FadeMonoScript : MonoBehaviour
    {
        public event Action OnFinishFade;
        public event Action OnFinishAppear;
        public void BeginFade(float timeStep, float colorStep, params MaskableGraphic[] uiElements)
        {
            if(this!=null)
                StartCoroutine(FadeEnumerator(uiElements, timeStep, colorStep));
        }

        public void BeginAppear(float timeStep, float colorStep, params MaskableGraphic[] uiElements)
        {
            StartCoroutine(AppearEnumerator(uiElements, timeStep, colorStep));
        }
        private IEnumerator FadeEnumerator(IEnumerable<MaskableGraphic> uiElements, float timeStep, float colorStep)
        {
            while (true)
            {
                foreach (MaskableGraphic uiElement in uiElements)
                {
                    uiElement.color = new Color(uiElement.color.r, uiElement.color.g, uiElement.color.b, uiElement.color.a - colorStep);
                }
                if (uiElements.All(x => x.color.a <= 0))
                {
                    OnFinishFade?.Invoke();
                    break;
                }
                yield return new WaitForSecondsRealtime(timeStep); //Realtime because timeScale can be = 0
            }
            yield break;
        }

        private IEnumerator AppearEnumerator(IEnumerable<MaskableGraphic> uiElements, float timeStep, float colorStep)
        {
            while (true)
            {
                foreach (MaskableGraphic uiElement in uiElements)
                {
                    uiElement.color = new Color(uiElement.color.r, uiElement.color.g, uiElement.color.b, uiElement.color.a + colorStep);
                }
                if (uiElements.All(x => x.color.a >= 1))
                {
                    OnFinishAppear?.Invoke();
                    break;
                }
                yield return new WaitForSecondsRealtime(timeStep); //Realtime because timeScale can be = 0
            }
            yield break;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
