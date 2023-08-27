using Entities;
using GameSystems;
using Model.Entities.Happenings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    public class HappeningManager
    {
        private HappeningCatalog happenCatalog;
        private QuestManager questManager;
        private HappeningReplaceManager replaceManager;
        private HappeningLauncher happeningLauncher;

        public event Action<Happening> OnLaunchHappening
        {
            add { happeningLauncher.OnBeginHappening += value; }
            remove { happeningLauncher.OnBeginHappening -= value; }
        }
        public event Action<Happening> OnFinishHappening
        {
            add { happeningLauncher.OnFinishHappening += value; }
            remove { happeningLauncher.OnFinishHappening -= value; }
        }
        public event Func<Task> OnFinishHappeningAsync
        {
            add { happeningLauncher.OnFinishHappeningAsync += value; }
            remove { happeningLauncher.OnFinishHappeningAsync -= value; }
        }

        [Inject]
        public void Construct(QuestManager questManager, HappeningCatalog happenCatalog, HappeningReplaceManager replaceManager, HappeningLauncher happeningLauncher)
        {
            this.questManager = questManager;
            this.happenCatalog = happenCatalog;
            this.replaceManager = replaceManager;
            this.happeningLauncher = happeningLauncher;
        }

        /// <summary>
        /// usual launch from queue by priority
        /// </summary>
        public void LaunchHappenFromQueue()
        {
            happeningLauncher.LaunchHappenFromQueue();
        }
        public void LaunchHappenFromQueue(int priority)
        {
            happeningLauncher.LaunchHappenFromQueue(priority);
        }

        /// <summary>
        /// fast launch without queue (for camp quests)
        /// </summary>
        /// <param name="questName"></param>
        public void LaunchHappenWithoutQueue(string questName)
        {
            if (GetHappeningFromQuestLine(questName, out Happening happening))
                happeningLauncher.LaunchHappenWithoutQueue(happening);
        }

        /// <summary>
        /// fast launch without queue and quest at all (debug)
        /// </summary>
        /// <param name="happenName"></param>
        public void LaunchHappenWithoutQuest(string happenName)
        {
            var happening = GetHappeningFromHappeningCatalog(happenName);
            happeningLauncher.LaunchHappenWithoutQueue(happening);
        }

        /// <summary>
        /// usual way to activate happening
        /// </summary>
        /// <param name="questName"></param>
        public void PutHappeningToQueue(string questName)
        {
            if (GetHappeningFromQuestLine(questName, out Happening happening))
                happeningLauncher.AddHappening(happening);
        }

        /// <summary>
        /// use for load game
        /// </summary>
        /// <param name="happenName"></param>
        public void PutHappenigToQueueForLoader(string happenName)
        {
            var happening = GetHappeningFromHappeningCatalog(happenName);
            happeningLauncher.AddHappening(happening);
        }

        public void CancelHappening(string questName)
        {
            var happeningName = questManager.GetHappening(questName);
            happeningLauncher.RemoveHappening(happeningName);
        }

        public IEnumerable<Happening> EnumerableHappenings()
        {
            foreach (var happen in happeningLauncher.EnumerableHappenings())
            {
                yield return happen;
            }
        }
        public void Clear()
        {
            happeningLauncher.ClearQueue();
        }
        private bool GetHappeningFromQuestLine(string questName, out Happening happening)
        {
            var happeningName = questManager.GetHappening(questName);
            if (string.IsNullOrEmpty(happeningName))
            {
                happening = null;
                return false;
            }
            happening = GetHappeningFromHappeningCatalog(happeningName);
            return true;
        }

        private Happening GetHappeningFromHappeningCatalog(string happeningName)
        {
            if (replaceManager.TryGetReplacement(happeningName, out string replacement))
            {
                return happenCatalog.GetHappening(replacement);
            }
            return happenCatalog.GetHappening(happeningName);
        }
    }
}
