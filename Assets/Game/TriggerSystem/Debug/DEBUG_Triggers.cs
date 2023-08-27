using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_Triggers : MonoBehaviour
{
    [SerializeField]
    private bool hide;

    private void ChangeTransparent(float transparent)
    {
        var childrens = GetComponentsInChildren<SpriteRenderer>();
        foreach (var child in childrens)
            child.material.color = new Color(child.material.color.r, child.material.color.g, child.material.color.b, transparent);
    }

    private void Update()
    {
        if (hide)
            ChangeTransparent(0);
        else
            ChangeTransparent(120);
    }

}
