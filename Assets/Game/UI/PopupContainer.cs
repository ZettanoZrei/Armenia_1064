using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PopupContainer : MonoBehaviour, ILateDisposable
{
    void ILateDisposable.LateDispose()
    {

    }

    private void OnDestroy()
    {
        
    }
}
