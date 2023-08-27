using Model.Entities.Phrases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Game.HappeningSystem
{
    class TextScrollView : MonoBehaviour
    {
        [SerializeField]
        private Transform content;

        [SerializeField]
        private Text textPref;

        [SerializeField]
        private ScrollRect scrollRect;

        [SerializeField]
        private bool IsDeletePastPhrase;


        public void UpdateView(Phrase phrase)
        {
            CreateTextSample(phrase.Text);
        }

        public void Finish()
        {
            Clear();
        }

        private async void CreateTextSample(string text)
        {
            if (IsDeletePastPhrase)
                Clear();

            var uiText = Instantiate(textPref, content);
            uiText.text = text;


            if (scrollRect is null)
                return;

            //use async because coroutine stops when time.timespan=0
            await Task.Delay(100);
            scrollRect.verticalNormalizedPosition = 0f;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)scrollRect.transform);
        }


        private void Clear()
        {
            foreach (Transform child in content)
                Destroy(child.gameObject);
        }
    }
}
