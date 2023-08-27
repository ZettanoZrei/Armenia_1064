using Assets.Game.Configurations;
using Assets.Game.Timer;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Assets.Game.Parameters.EndedParamSystem
{
    public class EndedParamMechanics : IInitializable, ILateDisposable, ITickable
    {
        private readonly TimeConfig timeConfig;
        private readonly ParametersConfig parametersConfig;

        public event Action<int> OnRemovingPeople;
        private readonly IReadOnlyDictionary<ParameterType, TimerScript> timers = new Dictionary<ParameterType, TimerScript>
        {
            {ParameterType.Food, new TimerScript() },
            {ParameterType.Spirit, new TimerScript() },
            {ParameterType.Stamina, new TimerScript() },
        };

        private readonly Queue<int> removingPeopleQueue = new Queue<int>();

        public IReadOnlyDictionary<ParameterType, TimerScript> Timers => timers;
        public bool IsPopupAvailable { get; set; } = true;
        private bool isEndedPermitted;
        public EndedParamMechanics(TimeConfig timeConfig, ParametersConfig parametersConfig)
        {
            this.timeConfig = timeConfig;
            this.parametersConfig = parametersConfig;
        }

        void IInitializable.Initialize()
        {
            foreach (var timer in timers.Values)
            {
                timer.OnElapsed += ActivateRemovingPeople;
            }
        }

        void ILateDisposable.LateDispose()
        {
            foreach (var timer in timers.Values)
            {
                timer.OnElapsed -= ActivateRemovingPeople;
                timer.Stop();
            }
        }

        void ITickable.Tick()
        {
            if (!isEndedPermitted)
                return;

            UpdateTimers();
            RemovePeople();
        }

        private void RemovePeople()
        {
            if (!IsPopupAvailable || !removingPeopleQueue.Any())
                return;

            var value = removingPeopleQueue.Dequeue();
            OnRemovingPeople?.Invoke(value);
        }
        private void UpdateTimers()
        {
            foreach (var timer in timers.Values)
            {
                timer.Update();
            }
        }

        private void ActivateRemovingPeople()
        {
            removingPeopleQueue.Enqueue(-parametersConfig.countPeopleRemoved);
        }
        public void BeginTimer(ParameterType parameterType)
        {
            Logger.WriteLog("BeginPeopleRemoving");

            ActivateRemovingPeople();
            var timer = timers[parameterType];
            var time = GetRemoveTime();
            timer.SetDuration(time);
            timer.Start();
        }

        public void FinishTimer(ParameterType parameterType)
        {
            var timer = timers[parameterType];
            timer.End();
        }

        public void StopTimers()
        {
            isEndedPermitted = false;
            //foreach (var timer in timers.Values)
            //    timer.Stop();
        }
        public void ResumeTimers()
        {
            isEndedPermitted = true;
            //foreach (var timer in timers.Values)
            //{
            //    if (timer.State == TimerState.Stoped)
            //        timer.Start();
            //}
        }

        public void Clear()
        {
            foreach (var timer in timers)
            {
                timer.Value.End();
            }
        }

        private float GetRemoveTime()
        {
            return timeConfig.secPerDay / parametersConfig.removePeopleTimes;
        }
    }
}
