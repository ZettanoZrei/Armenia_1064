using UnityEngine;

namespace Assets.Game.Scripts
{
    class TagComponent : MonoBehaviour, ITagComponent
    {
        [SerializeField]
        private TagMechanics tagMechanics;
        public Tag Tag => tagMechanics.Tag;

        public void ChangeTag(Tag tag)
        {
            tagMechanics.ChangeTag(tag);
        }
    }
}
