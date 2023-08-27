using Assets.Game.HappeningSystem;
using Assets.Modules.UI;
using Model.Entities.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Game.Camp
{
    class OptionComponentsNightWork : MonoBehaviour
    {
        [SerializeField] private Transform optionsContainer;
        [SerializeField] private OptionButton optionButtonPrefab;
        [SerializeField] private GameObject optionPoiner;
        public event Action<int?> OnDecitionMade;


        //ue TextScrollView
        public void UpdateOptions(List<NightWorkOptionInfo> options)
        {
            Enable();
            foreach (var option in options)
            {
                var button = Instantiate(optionButtonPrefab, optionsContainer);               
                var signComponent = button.gameObject.GetComponent<OptionSignComponent>();
                signComponent.SetParamForSigns(option);
                button.SetAnswerText(option.Answer.Text);
                button.AddListener(() => MadeDecitionHandler(option.Answer));
                button.OnOptionPointer += SetOptionPointer;
                SetRestriction(option.Answer, button);
                SetOptionPointer(button);
            }
        }

        private void SetOptionPointer(OptionButton optionButton)
        {
            optionButton.SetPointerPosition(optionPoiner.transform);
            optionPoiner.SetActive(true);
        }

        private void Enable()
        {
            optionsContainer.gameObject.SetActive(true);
        }

        private void DisableOptionContainer()
        {
            optionsContainer.gameObject.SetActive(false);
            optionPoiner.SetActive(false);
        }


        private void SetRestriction(Answer answer, OptionButton button)
        {
            if (answer.IsRestriction)
            {
                if (answer.Restriction.IsAvailable)
                    button.SetStatus(OptionButton.State.Available);
                else
                    button.SetStatus(OptionButton.State.Blocked);
            }
            else
            {
                button.SetStatus(OptionButton.State.NonRestriction);
            }
        }
        private void MadeDecitionHandler(Answer option)
        {
            Clear();
            DisableOptionContainer();
            this.OnDecitionMade?.Invoke(option.Index);
        }

        private void Clear()
        {
            optionPoiner.SetActive(false);
            optionPoiner.transform.parent = optionsContainer;

            //что бы кнопки не оставались в префабе после вызова
            foreach (Transform child in optionsContainer)
            {
                if (child.GetComponent<OptionButton>())
                    Destroy(child.gameObject);
            }
        }

        private void OnEnable()
        {
            optionPoiner.SetActive(false);
        }
    }
}
