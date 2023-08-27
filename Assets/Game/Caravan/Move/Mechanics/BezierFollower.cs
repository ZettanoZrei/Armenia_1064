using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BezierFollower : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField]
    [Range(0,4)]
    private float startPostion;
    public float Distance { get; set; }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        this.Distance = startPostion;
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {

    }
}
