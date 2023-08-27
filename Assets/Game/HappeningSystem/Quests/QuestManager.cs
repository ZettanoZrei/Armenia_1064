using GameSystems;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Model.Entities.Answers;
using Model.Entities.Persons;
using System.Collections;

namespace Assets.Game.HappeningSystem
{
    public class QuestManager : IEnumerable<Quest>
    {
        private readonly List<Quest> allQuests = new List<Quest>();
        private IEnumerable<Quest> campQuests => allQuests.Where(x => x.IsCampQuest);
        public void AddQuest(Quest quest)
        {
            if (!allQuests.Contains(quest))
                allQuests.Add(quest);
        }
        public void WriteDownQuestConsequences(SingleQuestConsequences questConsequences)
        {
            Logger.WriteLog($"questConsequences: quest - {questConsequences.Quest}, pointer - {questConsequences.Happening} ");
            var quest = GetQuest(questConsequences.Quest);
            quest.Pointer = questConsequences.Happening;
        }

        public string GetHappeningName(string questName)
        {
            var quest = GetQuest(questName);
            return quest.Pointer;
        }

        public string GetHappening(string questName)
        {
            var quest = GetQuest(questName);
            Logger.WriteLog($"Get Happening {quest.Pointer} in quest {questName}");
            return quest.Pointer;
        }

        public void Clear()
        {
            allQuests.Clear();
        }
        private Quest GetQuest(string questName)
        {
            return allQuests.First(x => x.Title == questName);
        }

        public IEnumerable<Quest> GetAvailableCampQuest()
        {
            return campQuests.Where(x => !x.Pointer.Equals("-----")); //обозначениe пустого события
        }

        public IEnumerator<Quest> GetEnumerator()
        {
            return ((IEnumerable<Quest>)allQuests).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)allQuests).GetEnumerator();
        }
    }
}


