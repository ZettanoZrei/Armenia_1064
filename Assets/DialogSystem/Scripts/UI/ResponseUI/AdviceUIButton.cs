using Assets.Game.HappeningSystem.Persons;
using PixelCrushers.DialogueSystem.Wrappers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.DialogSystem.Scripts.UI.ResponseUI
{
    public class AdviceUIButton : StandardUIResponseButton, IMyResponseUI
    {
        private RelationComponent relationComponent;
        [SerializeField] private Image image;
        public override void Awake()
        {
            base.Awake();
            relationComponent = GetComponent<RelationComponent>();
        }


        void IMyResponseUI.SetState(bool enabled)
        {
            //this.enabled = enabled;
            button.enabled = enabled;
            //todo implement view
        }

        public void SetRelation(int value)
        {
            relationComponent.SetRelationLevel(value);
        }

        public void SetPortrait(Sprite sprite)
        {
            image.sprite = sprite;
        }
    }
}
