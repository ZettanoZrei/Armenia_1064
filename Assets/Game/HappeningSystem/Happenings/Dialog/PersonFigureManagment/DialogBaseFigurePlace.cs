using Model.Types;
using UnityEngine;
using Cinemachine;

namespace Assets.Game.HappeningSystem
{
    public class DialogBaseFigurePlace : MonoBehaviour
    {
        [SerializeField] protected PositionType orientation;
        [SerializeField] protected CinemachineVirtualCamera virtualCamera;
        [SerializeField] protected CinemachineConfiner confiner;
        [SerializeField] protected string _name;
        public void FocusCamera()
        {
            virtualCamera.Priority = 10;
        }

        public void UnfocusCamera()
        {
            virtualCamera.Priority = 1;
        }

        public DialogBaseFigurePlace SetPosition(int x)
        {
            transform.localPosition = new Vector3(x, 0, 0);
            return this;
        }

        public DialogBaseFigurePlace SetBound(Collider2D collider)
        {
            confiner.m_BoundingShape2D = collider;
            return this;
        }

        public DialogBaseFigurePlace SetName(int number)
        {
            confiner.gameObject.name = _name + number;
            return this;
        }
    }
}
