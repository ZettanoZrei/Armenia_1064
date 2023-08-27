using UnityEngine;

namespace Assets.Game.HappeningSystem
{
    public class PersonModelViewComponent : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer frontView;
        [SerializeField] private SpriteRenderer backView;

        private const int backStartLayer = 10;
        private const int frontStartLayer = 4;

        //un init
        public void GetPack(Sprite front, Sprite back, int layer)
        {
            this.frontView.sprite = front;
            this.backView.sprite = back;

            this.backView.sortingOrder= backStartLayer + layer;
            this.frontView.sortingOrder = frontStartLayer + layer;  
        }
    }
}
