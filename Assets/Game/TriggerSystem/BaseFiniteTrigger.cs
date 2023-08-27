using UnityEngine;

namespace Assets.Game.HappeningSystem
{
    public abstract class BaseFiniteTrigger : MonoBehaviour
    {
        public bool IsDone { get; set; }
        public int Index { get; set; }

        protected virtual void EnterCaravanAction() { }
        protected virtual void ExitCaravanAction() { }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "caravan" && !IsDone)
            {
                EnterCaravanAction();
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.tag == "caravan" && !IsDone)
            {
                ExitCaravanAction();
            }
        }
    }
}
