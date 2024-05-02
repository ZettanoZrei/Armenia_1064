using Model.Types;
using Newtonsoft.Json.Linq;
using PixelCrushers.DialogueSystem.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.DialogSystem.Scripts.UI
{
    public class ConclusionDialogueUI : StandardDialogueUI
    {
        [SerializeField] private List<MessageParamView> messageParamViews;
        [SerializeField] private MessageRelationView relationPrefab;
        [SerializeField] private Transform containerPerson;
        [SerializeField] private Transform containerParams;

        public override void Awake()
        {
            base.Awake();

            if (containerPerson)
                containerPerson.gameObject.SetActive(false);

            if (containerParams)
                containerParams.gameObject.SetActive(false);
        }

        public void SetParameters(Dictionary<ParameterType, int> parameters)
        {
            foreach(var parameter in parameters)
            {
                var view = messageParamViews.First(x => x.ParameterType == parameter.Key);
                view.SetParam(parameter.Value);
            }
            if (!containerParams.gameObject.activeSelf)
                containerParams.gameObject.SetActive(true);
        }

        public void SetRelations(Dictionary<string, int> persons)
        {
            foreach (var person in persons)
            {
                var messageRelationPanel = Instantiate(relationPrefab, containerPerson);
                messageRelationPanel.SetRelation(person.Key, person.Value);

                if (!containerPerson.gameObject.activeSelf)
                    containerPerson.gameObject.SetActive(true);
            }
            
        }
    }
    
}
