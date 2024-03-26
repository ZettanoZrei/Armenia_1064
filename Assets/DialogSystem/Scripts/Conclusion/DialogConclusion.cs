using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class DialogConclusion
{
    public int food;
    public int stamina;
    public int blessing;
    public int people;
    public List<PersonResult> persons = new List<PersonResult>();


    [Serializable]
    public class PersonResult
    {
        public string actor;
        public List<int> relations = new List<int>();
    }

    public void Nullify()
    {
        //food = 0; 
        //stamina = 0; 
        //blessing = 0; 
        //people = 0;
        //persons = new List<PersonResult>();
    }
}


