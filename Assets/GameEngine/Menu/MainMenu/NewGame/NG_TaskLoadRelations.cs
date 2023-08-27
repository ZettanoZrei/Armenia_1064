using Assets.Game.HappeningSystem;
using Assets.Game.HappeningSystem.Persons;
using Model.Entities;
using Model.Entities.Info;
using Model.Serializers;
using UnityEngine;
using Zenject;

namespace Loader
{
    public class NG_TaskLoadRelations : ING_Task
    {
        private readonly TextAsset source;
        private readonly RelationManager relationManager;
        private readonly DialogPersonPackCatalog personPacks;
        private readonly BinSerializer<InfoToUnity> binSerializer;

        public NG_TaskLoadRelations(RelationManager relationManager, DialogPersonPackCatalog personPacks)
        {           
            this.relationManager = relationManager;
            this.personPacks = personPacks;
            source = Resources.Load<TextAsset>("Source/Info/infoForUnity");
            binSerializer = new BinSerializer<InfoToUnity>();
        }

        void ING_Task.Execute()
        {
            var data = binSerializer.DeserializeFromMemory(new System.IO.MemoryStream(source.bytes));
            foreach (var person in data.Persons)
            {
                var pack = personPacks.GetPack(person);
                relationManager.InitPersonRelation(person, pack.Relation);
            }
        }
    }
}
