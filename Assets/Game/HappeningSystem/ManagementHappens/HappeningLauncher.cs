using Assets.Game.HappeningSystem.ManagementHappens;
using Assets.GameEngine;
using Assets.Modules;
using Model.Entities.Happenings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    public class HappeningLauncher : ICallBack, ILeaveGameComponentDI
    {
        private readonly List<Happening> happeningsQueue = new List<Happening>();

        public event Action<Happening> OnBeginHappening;
        public event Action<Happening> OnFinishHappening;
        public event Func<Task> OnFinishHappeningAsync;
        private HappeningManagerFactory managerFactory;
        private ManagerTypeResolver typeResolver;
        private bool isHappeningActive;

        public bool IsHappeningActive => isHappeningActive; 

        [Inject]
        public void Construct(HappeningManagerFactory managerFactory, ManagerTypeResolver typeResolver, GameSystemDIController systemDIController)
        {
            this.managerFactory = managerFactory;
            this.typeResolver = typeResolver;
            systemDIController.AddComponent(this);
        }
        public void AddHappening(Happening happening)
        {
            if (!happeningsQueue.Any(x => x.Title == happening.Title))
                happeningsQueue.Add(happening);
        }

        public void RemoveHappening(string title)
        {
            var happening = happeningsQueue.FirstOrDefault(x => x.Title == title);
            if (happening != null)
                happeningsQueue.Remove(happening);
        }

        public void LaunchHappenFromQueue()
        {
            if (isHappeningActive || !happeningsQueue.Any())
                return;

            var theMostPriority = this.happeningsQueue.Where(x => x.Priority == 1 || x.Priority == 2).OrderBy(x => x.Priority).FirstOrDefault();
            if (theMostPriority == null)
                return;
            LaunchHappening(theMostPriority);
        }

        public void LaunchHappenFromQueue(int priority)
        {
            if (isHappeningActive || !happeningsQueue.Any(x => x.Priority == priority))
                return;

            var happening = this.happeningsQueue.Where(x => x.Priority == priority).First();
            LaunchHappening(happening);
        }

        public void LaunchHappenWithoutQueue(Happening happening)
        {
            LaunchHappening(happening);
        }

        public IEnumerable<Happening> EnumerableHappenings()
        {
            foreach (var happen in happeningsQueue)
            {
                yield return happen;
            }
        }

        public void ClearQueue()
        {
            happeningsQueue.Clear();
        }
        private void LaunchHappening(Happening happening)
        {
            if (happeningsQueue.Contains(happening))
                happeningsQueue.Remove(happening);

            typeResolver.DefineHappenType(happening);
            var manager = managerFactory.Create();
            manager.Init(happening, this);
            manager.Perform();

            OnBeginHappening?.Invoke(happening);
            isHappeningActive = true;
        }


        void ICallBack.Return(object data)
        {
            OnFinishHappening?.Invoke((Happening)data);
            OnFinishHappeningAsync?.Invoke();
            isHappeningActive = false;
        }

        void ILeaveGameComponentDI.LeaveGame()
        {
            isHappeningActive = false;
        }
    }
}
