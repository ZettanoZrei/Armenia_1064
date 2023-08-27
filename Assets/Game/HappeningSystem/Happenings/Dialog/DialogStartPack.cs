using Model.Entities.Persons;
using Model.Entities.Phrases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.HappeningSystem.View.Dialog
{
    public class DialogStartPack
    {
        public List<Person> StartPersons { get; set; } = new List<Person>();
        public Person Advisor { get; set; } = new Person();
        public DialogPhrase FirstPhrase { get; set; } = new DialogPhrase();
        public Sprite FrontImage { get; set; }
        public Sprite BackImage { get; set; }
        
    }
}
