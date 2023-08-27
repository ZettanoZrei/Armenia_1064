using Assets.Game;
using DG.Tweening;
using UnityEngine;

class MonoTest : Popup
{
    private void Start()
    {
        transform.DOMoveY(transform.position.y + 10, 5);
    }
}
