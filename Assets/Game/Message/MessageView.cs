using Assets.Game.HappeningSystem;
using Assets.Game.UI;
using System;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Message
{
    class MessageView : Popup
    {
        [SerializeField] private TextProView textAlignment;

        [SerializeField] private MessageParamView foodView;
        [SerializeField] private MessageParamView spiritView;
        [SerializeField] private MessageParamView staminaView;
        [SerializeField] private MessageParamView peopleView;

        [SerializeField] private MessageRelationView relationPrefab;
        [SerializeField] private Transform containerPerson;
        [SerializeField] private Transform containerText;
        [SerializeField] private Transform containerParams;

        public event Action OnClose;

        private void Awake()
        {
            if(containerPerson)
                containerPerson.gameObject.SetActive(false);

            if (containerText)
                containerText.gameObject.SetActive(false);

            if (containerParams)
                containerParams.gameObject.SetActive(false);
        }
        public void SetMessage(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            var value = text.Trim();

            //message.text += $"{value} \n";
            if (!containerText.gameObject.activeSelf)
                containerText.gameObject.SetActive(true);

            textAlignment.AssignString(value);
        }


        public void SetPersons(string name, int value)
        {           
            var messageRelationPanel = Instantiate(relationPrefab, containerPerson);
            messageRelationPanel.SetRelation(name, value);

            if (!containerPerson.gameObject.activeSelf)
                containerPerson.gameObject.SetActive(true);
        }

        public void SetFood(int value)
        {           
            foodView.SetParam(value);

            if (!containerParams.gameObject.activeSelf)
                containerParams.gameObject.SetActive(true);
        }

        public void SetSpirit(int value)
        {
            spiritView.SetParam(value);

            if (!containerParams.gameObject.activeSelf)
                containerParams.gameObject.SetActive(true);
        }

        public void SetStamina(int value)
        {
            staminaView.SetParam(value);

            if (!containerParams.gameObject.activeSelf)
                containerParams.gameObject.SetActive(true);
        }

        public void SetPeople(int value)
        {
            peopleView.SetParam(value);

            if (!containerParams.gameObject.activeSelf)
                containerParams.gameObject.SetActive(true);
        }

        //ui close button
        public void Close()
        {
            OnClose?.Invoke();
        }
    }
}
