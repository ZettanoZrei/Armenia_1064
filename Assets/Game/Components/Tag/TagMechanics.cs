using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Game.Scripts
{
    class TagMechanics : MonoBehaviour
    {
        private GameObject root;
        public Tag Tag { get; private set; }

        [SerializeField]
        private Tag _tag;
        internal void ChangeTag(Tag tag)
        {
            Tag = tag;
            root.tag = tag.ToString();
        }

        private void Awake()
        {
            var root = transform.GetComponentInParent<MonoEntity>();
            Tag = root.tag.ToTag();
            _tag = Tag;
        }

        private void OnValidate()
        {

        }
    }
}
