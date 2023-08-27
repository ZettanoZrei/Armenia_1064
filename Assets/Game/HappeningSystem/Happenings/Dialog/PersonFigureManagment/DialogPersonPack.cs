using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.HappeningSystem
{
    [CreateAssetMenu(fileName = "DialogPersonPack", menuName = "View/DialogPersonPack")]
    public class DialogPersonPack : ScriptableObject
    {
        [SerializeField]
        private string _name;

        [SerializeField] private List<FigureComplect> complects;
        [SerializeField] private List<Sprite> portraits;

        [SerializeField]
        private int relation;

        [SerializeField] private bool mainCharacter;

        public string Name => _name;       
        public Sprite Front =>  complects[ComplectIndex].front;
        public Sprite Back => complects[ComplectIndex].back;
        public Sprite Portret => portraits[PortraitIndex];
        public int Relation => relation;
        public bool MainCharacter => mainCharacter;

        [NonSerialized, HideInInspector] public int complectIndex;
        [NonSerialized, HideInInspector] public int portraitIndex;

        private int ComplectIndex => complectIndex < complects.Count ? complectIndex : 0;
        private int PortraitIndex => portraitIndex < portraits.Count ? portraitIndex : 0;
    }

    [Serializable]
    class FigureComplect
    {
        public Sprite front;
        public Sprite back;
    }
}
