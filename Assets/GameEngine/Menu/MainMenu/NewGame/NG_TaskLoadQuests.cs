using System;
using System.Collections;
using Assets.Game.HappeningSystem;
using Model.Entities;
using Model.Entities.Info;
using Model.Serializers;
using UnityEngine;
using Zenject;

namespace Loader
{
    public class NG_TaskLoadQuests : ING_Task
    {
        private readonly TextAsset source;
        private readonly BinSerializer<InfoToUnity> binSerializer;
        private readonly QuestManager questManager;

        public NG_TaskLoadQuests(QuestManager questManager)
        {
            source = Resources.Load<TextAsset>("Source/Info/infoForUnity");
            //var pa = Application.persistentDataPath;

            binSerializer = new BinSerializer<InfoToUnity>();
            this.questManager = questManager;
        }

        void ING_Task.Execute()
        {
            var data = binSerializer.DeserializeFromMemory(new System.IO.MemoryStream(source.bytes));
            foreach (var item in data.Quests)
            {
                var quest = new Quest(item.IsRequired, item.Title, item.Pointer, item.IsCampQuest, item.PersonName);
                questManager.AddQuest(quest);
            }
        }
    }
}
