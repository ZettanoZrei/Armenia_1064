using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

class ReactionPart : MonoBehaviour, IPoolable<string, IMemoryPool>, IDisposable
{
    [SerializeField] private Text uText;
    private IMemoryPool pool;
    public void Dispose()
    {
        pool.Despawn(this);
    }
    public void OnSpawned(string p1, IMemoryPool p2)
    {
        uText.text = p1;
        pool = p2;
        transform.position = Vector3.zero;         
    }

    public void OnDespawned()
    {
        pool = null;
        uText.text = string.Empty;
    }

    public void TransformNullify()
    {
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    public class Factory : PlaceholderFactory<string, ReactionPart>
    {
    }
}


