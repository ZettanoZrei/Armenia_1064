using System;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Camp
{
    class BlackPanelPopup : Popup
    {
        public event Action<object> OnFinish;
        private Image image;

        private void OnEnable()
        {
            image = GetComponent<Image>();
        }
        public void Show(float darkUp, float darkDown, float blackoutTime)
        {
            StartCoroutine(DarkUp(darkUp, darkDown, blackoutTime));
        }

        private IEnumerator DarkUp(float darkUp, float darkDown, float blackoutTime)
        {
            while (true)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + darkUp);
                //print(image.color.a);
                if(image.color.a >= 1)
                {
                    break;
                }
                yield return null;
            }

            yield return new WaitForSecondsRealtime(blackoutTime);

            while (true)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - darkDown);
                //print(image.color.a);
                if (image.color.a <= 0)
                {
                    break;
                }
                yield return null;
            }
            OnFinish?.Invoke(null);
        }

       
        public void Stop()
        {
            StopAllCoroutines();
        }
    }
}
