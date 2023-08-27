using UnityEngine;
using System;

namespace Assets.Game.Timer
{
    public class TimeMechanics
    {
        public int Days { get; private set; }
        public float Time { get; private set; }

        public event Action<float> OnTimeChanged;
        public event Action<int> OnDayChanged;

        public void AddDay(float value)
        {
            Time += value;
            if (Time >= 1)
            {
                var days = Mathf.FloorToInt(Time);
                Time -= days;
                Days += days;

                OnDayChanged?.Invoke(Days);
            }

            OnTimeChanged?.Invoke(Time);
        }

        public void SetDays(float value)
        {
            Days = Mathf.FloorToInt(value);
            Time = value - Days;

            OnDayChanged?.Invoke(Days);
            OnTimeChanged?.Invoke(Time);
        }

    }
}
