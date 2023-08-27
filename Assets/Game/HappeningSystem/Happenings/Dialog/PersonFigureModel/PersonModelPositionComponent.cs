using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Game.HappeningSystem
{
    public class PersonModelPositionComponent : MonoBehaviour
    {
        [SerializeField] private Transform frontView;
        [SerializeField] private Transform backView;

        public void SetFrontViewPosition(Vector3 vector)
        {
            frontView.position = new Vector3(vector.x, vector.y - 0.8f, vector.z); //vector;
        }
        public void SetBackViewPosition(Vector3 vector)
        {
            backView.position = new Vector3(vector.x, vector.y - 1.3f, vector.z);
        }
    }
}
