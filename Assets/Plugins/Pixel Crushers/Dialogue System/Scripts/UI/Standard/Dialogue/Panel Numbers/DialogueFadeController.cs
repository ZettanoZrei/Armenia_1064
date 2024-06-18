using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFadeController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;
    public float Duration = 0f;
    void Start()
    {
        _animator= this.GetComponent<Animator>();
    }

    public void Play()
    {
        if (_animator != null)
        {
            _animator.speed = Duration;
            _animator.Play("Fade");
        }
    }
}
