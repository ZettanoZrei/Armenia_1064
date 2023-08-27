using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CaravanService : MonoBehaviour
{
    [SerializeField]
    private MonoEntity caravan;
    public MonoEntity Caravan => caravan;
}

