using Model.Entities.Answers;
using Model.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem.Persons
{
    internal class Debug_Relation : MonoBehaviour
    {
        [Inject] RelationManager relationManager;

        public string person;
        public int value;

        [ContextMenu("ChangeRelation")]
        public void ChangeRelation()
        {
            relationManager.ChangeRelation(new SinglePersonConsequences { PersonName = new PersonName(person), Value = value });
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.O)) 
            {
                foreach (var rel in relationManager)
                    print($"{rel.Name}: {rel.Value}");
            }
        }
    }
}
