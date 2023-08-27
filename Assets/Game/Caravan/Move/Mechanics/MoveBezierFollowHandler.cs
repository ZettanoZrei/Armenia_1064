using Assets.Game.Configurations;
using PathCreation;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Zenject;


[ExecuteInEditMode]
public class MoveBezierFollowHandler : MonoBehaviour
{
    [SerializeField] private bool debugMode;
    [Space(10)]
    [SerializeField] private MoveMechanics moveMechanics;
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private BezierFollow.Direction direction;
    [SerializeField] private List<BezierFollower> bezierFollowers = new List<BezierFollower>();
    internal List<BezierFollower> BezierFollows => bezierFollowers;

    [Inject] private CaravanConfig caravanConfig;
    public float SpeedModifier => caravanConfig.speed / 10;
    public bool IsMoving => moveMechanics.IsMoving;

    private BezierFollow bezierFollow;

    private void Start()
    {
        bezierFollow = new BezierFollow(pathCreator, direction);
    }


    private void FixedUpdate()
    {
        Moving();

#if DEBUG
        ////для растановки людей в каравне
        //if (debugMode)
        //{
        //    MoveFrame();
        //    print("DebugMode!");
        //}
#endif
    }


    [ContextMenu("MoveFrame")]
    public void MoveFrame()
    {
        var bezierFollow = new BezierFollow(pathCreator, direction);
        foreach (var elem in bezierFollowers)
            bezierFollow.Move(elem, 0);
    }
    private void Moving()
    {
        if (moveMechanics.IsMoving)
        {
            foreach (var elem in bezierFollowers)
                bezierFollow.Move(elem, SpeedModifier);
        }
    }
}

