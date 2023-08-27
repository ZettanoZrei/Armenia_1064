using Assets.Game.HappeningSystem;
using Model.Entities.Happenings;
using Model.Serializers;
using UnityEngine;
using Zenject;

namespace Loader
{
    public class TaskLoadHappenings : IInitializable
    {
        private readonly TextAsset[] source;
        private readonly HappeningCatalog catalog;
        private readonly JsonSerializer<Happening> jsonSerializer;

        public TaskLoadHappenings(HappeningCatalog catalog)
        {           
            this.source = Resources.LoadAll<TextAsset>("Source/Happenings");
            this.jsonSerializer = new JsonSerializer<Happening>();
            this.catalog = catalog;
        }
        void IInitializable.Initialize()
        {
            foreach (var item in this.source)
            {
                var data = this.jsonSerializer.Deserialize(item.text);
                catalog.AddHappening(data);
            }
        }
    }
}
