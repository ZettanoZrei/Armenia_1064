using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Game.UI.TimeUI
{
    internal class TimeView : MonoBehaviour
    {
        [SerializeField] private ProgressBar time;
        [SerializeField] private Text days;

        [SerializeField] private UnityEvent OnSetDay;
        public void SetTime(float value)
        {
            time.SetProgress(value);
        }

        public void SetDay(int value)
        {
            var oldValue = days.text;
            days.text = value.ToString();

            if (string.IsNullOrEmpty(oldValue))
                return;
            OnSetDay?.Invoke();
        }
    }
}
