using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

class DialogResult : MonoBehaviour
{
    [Range(0, 100)] public int food;
    [Range(0, 100)] public int stamina;
    [Range(0, 10)] public int blessing;
    [Range(0, 1000)] public int people;
    public List<PersonResult> persons = new List<PersonResult>();

    [Serializable]
    public class PersonResult
    {
        public Actor actor;
        public List<int> relations = new List<int>();
    }

    public void Nullify()
    {
        food = 0; 
        stamina = 0; 
        blessing = 0; 
        people = 0;
        persons = new List<PersonResult>();
    }
}


