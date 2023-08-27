using System;
using UnityEngine;

namespace Interfeces
{
    interface ITriggerComponent
    {
        event Action<Collider2D> OnTriggerEnter;
        event Action<Collider2D> OnTriggerExit;
        event Action<Collider2D> OnTriggerStay;
    }
    
}
