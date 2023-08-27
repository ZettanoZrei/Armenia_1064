using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Camp.Background
{
    public class CampBackground : MonoBehaviour
    {
        [SerializeField] private RectTransform[] fields;
        public RectTransform[] Fields => fields; 
    }
}
