using Assets.Modules.UI;
using Model.Entities.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Assets.Game.HappeningSystem
{
    class OptionComponent : MonoBehaviour
    {
        [SerializeField] private Transform optionsContainer;
        [SerializeField] private OptionButton optionButtonPrefab;
        [SerializeField] private SimpleButton closeButton;
        [SerializeField] private GameObject closeButtonContainer;
        [SerializeField] private GameObject optionPoiner;
        public event Action<int> OnDecitionMade;


        public void UpdateOptions(List<Answer> options)
        {
            EnableOptionContainer();
            foreach (var answer in options)
            {
                CreateOptionButton(answer);
            }
        }

        private void CreateOptionButton(Answer answer)
        {
            var button = Instantiate(optionButtonPrefab, optionsContainer);
            button.SetAnswerText(answer.Text);
            button.AddListener(() => MadeDecitionHandler(answer));
            button.OnOptionPointer += SetOptionPointer;
            SetRestriction(answer, button);
            SetOptionPointer(button);
        }

        private void SetOptionPointer(OptionButton optionButton)
        {
            optionButton.SetPointerPosition(optionPoiner.transform);
            optionPoiner.SetActive(true);
        }
        private void EnableOptionContainer()
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
                button.SetRestrictionText(answer.Restriction.Value);
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
            OnDecitionMade?.Invoke(option.Index);
        }

        private void Clear()
        {
            optionPoiner.SetActive(false);
            optionPoiner.transform.SetParent(optionsContainer);

            //чтобы кнопки не оставались в префабе после вызова
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
