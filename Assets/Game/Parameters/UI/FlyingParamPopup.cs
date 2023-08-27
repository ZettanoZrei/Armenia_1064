using Assets.Game.Configurations;
using DG.Tweening;
using Model.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.UI.FlyingParamPopup
{
    internal class FlyingParamPopup : Popup
    {
        [SerializeField] private Sprite peopleSprite;
        [SerializeField] private Sprite foodSprite;
        [SerializeField] private Sprite spiritSprite;
        [SerializeField] private Sprite staminaSprite;
        [SerializeField] private Image uImage;
        [SerializeField] private Text uText;


        [SerializeField] private MessageParamView messageParamView;
        private PopupConfig.FlyinngPopup config;
        public event Action OnFinish;
        private float limit;

        private void Start()
        {
            limit = transform.position.y + config.upLimit;
            StartCoroutine(Up());
        }

        private void OnDestroy()
        {
            OnFinish?.Invoke();
        }
        private IEnumerator Up()
        {
            while (true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + config.speed, transform.position.z);
                uText.color = new Color(uText.color.r, uText.color.g, uText.color.b, uText.color.a - config.fadeSpeed);
                uImage.color = new Color(uImage.color.r, uImage.color.g, uImage.color.b, uImage.color.a - config.fadeSpeed);

                if (transform.position.y > limit || uText.color.a <= 0)
                {                   
                    break;
                }
                yield return new WaitForSecondsRealtime(0.03f);
            }
            OnFinish?.Invoke();
        }

        public FlyingParamPopup SetConfig(PopupConfig.FlyinngPopup config)
        {
            this.config = config;
            return this;
        }
        public FlyingParamPopup SetParam(int value)
        {
            messageParamView.SetParam(value);
            return this;
        }

        public FlyingParamPopup SetActive(bool active)
        {
            messageParamView.SetActive(active);
            return this;
        }

        public FlyingParamPopup SetImage(ParameterType parameter)
        {
            switch (parameter)
            {
                case ParameterType.Food:
                    uImage.sprite = foodSprite;
                    break;
                case ParameterType.Spirit:
                    uImage.sprite = spiritSprite;
                    break;
                case ParameterType.Stamina:
                    uImage.sprite = staminaSprite;
                    break;
                case ParameterType.People:
                    uImage.sprite = peopleSprite;
                    break;
                default: throw new ArgumentException();
            }
            return this;
        }
    }
}
