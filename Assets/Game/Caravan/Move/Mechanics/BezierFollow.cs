using PathCreation;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow
{
    private readonly PathCreator pathCreator;
    private Dictionary<Direction, float> angles = new Dictionary<Direction, float>
    {
        { Direction.Left, 0f },
        { Direction.Right, 180f },
    };

    private float angle;
    public BezierFollow(PathCreator pathCreator, Direction direction)
    {
        this.pathCreator = pathCreator;
        this.angle = angles[direction];
    }

    public void Move(BezierFollower bezierFollower, float speed)
    {
        bezierFollower.Distance += speed * Time.fixedDeltaTime;
        bezierFollower.transform.position = pathCreator.path.GetPointAtDistance(bezierFollower.Distance);
        bezierFollower.transform.rotation = Rotate(bezierFollower.Distance);
    }


    private Quaternion Rotate(float dinstance)
    {
        float t = dinstance / pathCreator.path.length;
        Vector3 xDirection = pathCreator.path.GetDirection(t).normalized;

        // Y axis is 90 degrees away from the X axis
        Vector3 yDirection = Quaternion.Euler(angle, angle, -90) * xDirection; // 0, 0, -90 or 180,180,-90

        // Z should stay facing forward for 2D objects
        Vector3 zDirection = Vector3.forward;

        // apply the rotation to this object
        return Quaternion.LookRotation(zDirection, yDirection);
    }

    public enum Direction
    {
        Left = 0,
        Right = 180
    }
}
