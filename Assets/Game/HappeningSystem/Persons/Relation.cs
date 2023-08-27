using UniRx;

namespace Assets.Game.HappeningSystem.Persons
{
    public class Relation
    {
        public string Name { get; set; }
        public ReactiveProperty<int> Value { get; set; } = new ReactiveProperty<int>();
    }
}
