using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RelationComponent : MonoBehaviour
{
    [SerializeField] private List<GameObject> relationLevels;
    [SerializeField] protected GameObject relationPanel;
    private int relationRestriction = 3;
    [SerializeField] private UnityEvent<bool> OnSetMode;


    public void SetRelationLevel(int value)
    {
        for (var i = 0; i < relationLevels.Count; i++)
        {
            if (value >= i + 1)
                relationLevels[i].SetActive(true);
            else
                relationLevels[i].SetActive(false);
        }
    }

    private void SetState(int relationValue)
    {
        var value = relationValue < relationRestriction;
        OnSetMode?.Invoke(value);
    }
}


