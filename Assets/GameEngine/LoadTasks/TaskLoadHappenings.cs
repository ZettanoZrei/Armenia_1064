using Assets.Game.Core;
using Assets.Game.HappeningSystem;
using Model.Entities.Happenings;
using Model.Serializers;
using System;
using UnityEngine;
using Zenject;

namespace Loader
{
    public class TaskLoadHappenings : IStep<LoadStepType>
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

        public LoadStepType StepType => LoadStepType.LoadHappening;

        public event Action OnFinishStep;
        public event Action<IStep<LoadStepType>> OnLaunchStep;

        public void Begin()
        {
            foreach (var item in this.source)
            {
                var data = this.jsonSerializer.Deserialize(item.text);
                catalog.AddHappening(data);
            }
        }

        public void Finish()
        {

        }
    }
}
