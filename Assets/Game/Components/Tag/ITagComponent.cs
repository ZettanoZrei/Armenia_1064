namespace Assets.Game.Scripts
{
    interface ITagComponent
    {
        public Tag Tag { get; }

        void ChangeTag(Tag tag);
    }
}
