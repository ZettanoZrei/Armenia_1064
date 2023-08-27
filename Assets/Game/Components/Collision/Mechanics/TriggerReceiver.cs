using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReceiver : MonoBehaviour
{
    public Action<Collider2D> OnTriggerEnterEvent;
    public Action<Collider2D> OnTriggerExitEvent;
    public Action<Collider2D> OnTriggerStayEvent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnterEvent?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExitEvent?.Invoke(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnTriggerStayEvent?.Invoke(collision);
    }
}
