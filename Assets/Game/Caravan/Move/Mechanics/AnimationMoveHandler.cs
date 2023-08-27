using System.Collections.Generic;
using UnityEngine;

public class AnimationMoveHandler: MonoBehaviour
{
    [SerializeField]
    private List<Animator> animations = new List<Animator>();

    [SerializeField]
    private MoveMechanics moveMechanics;

    public void PlayAnima()
    {
        foreach (var animator in animations)
        {
            animator.SetBool("ismove", true);
        }
    }

    public void StopAnima()
    {
        foreach (var animator in animations)
        {
            animator.SetBool("ismove", false);
        }
    }

    private void OnEnable()
    {
        moveMechanics.OnMovingEvent += PlayAnima;
        moveMechanics.OnFinishMovingEvent += StopAnima;
    }

    private void OnDisable()
    {
        moveMechanics.OnMovingEvent -= PlayAnima;
        moveMechanics.OnFinishMovingEvent -= StopAnima;
    }
}
