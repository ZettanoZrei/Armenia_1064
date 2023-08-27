using Assets.Game.UI;
using ModestTree.Util;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Assets.Game.HappeningSystem
{
    class OptionButton : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField]
        private Color blockAnswerColor;

        [SerializeField]
        private Color blockRestrictionColor;

        [SerializeField]
        private Color normalRestrictionColor;

        [SerializeField]
        private Color normalAnswerColor;

        [SerializeField]
        private Button button;

        [SerializeField]
        private TextUI answerText;

        [SerializeField]
        private Text restrictionText;

        [SerializeField]
        private GameObject restrictionParent;

        [SerializeField]
        private Transform pointerPlace;

        public event Action<OptionButton> OnOptionPointer;

        private void OnEnable()
        {
            this.blockAnswerColor.a = 255;
            this.blockRestrictionColor.a = 255;
            this.normalAnswerColor.a = 255;     
            this.normalRestrictionColor.a = 255;
        }

        public void AddListener(UnityAction action)
        {
            button.onClick.AddListener(action);
        }

        public void SetAnswerText(string text)
        {
            answerText.SetText(text.Trim()); 
        }

        public void SetPointerPosition(Transform poiner)
        {
            poiner.SetParent(pointerPlace);
            poiner.localPosition = Vector3.zero;
        }
        public void SetRestrictionText(int value)
        {
            restrictionText.text = $"{value}";
        }


        public void SetStatus(State state)
        {
            if(state == State.Blocked)
            {
                restrictionParent.gameObject.SetActive(true);
                answerText.SetTextColor(blockAnswerColor);
                restrictionText.color = blockRestrictionColor;
                button.interactable = false;
            }
            else if(state == State.Available)
            {
                restrictionParent.gameObject.SetActive(true);
                answerText.SetTextColor(normalAnswerColor);
                restrictionText.color = normalRestrictionColor;
                button.interactable = true;
            }
            else if(state == State.NonRestriction)
            {
                restrictionParent.gameObject.SetActive(false);
                answerText.SetTextColor(normalAnswerColor);
                button.interactable = true;
            }
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            OnOptionPointer?.Invoke(this);
        }

        public enum State
        {
            Available,
            Blocked,
            NonRestriction
        }
    }
}
