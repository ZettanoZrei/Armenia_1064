using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFadaController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;
    public float Duration = 0.1f;
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
