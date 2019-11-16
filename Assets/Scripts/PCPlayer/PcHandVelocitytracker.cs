using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcHandVelocitytracker : HandVelocityTracker
{
    [SerializeField] private float shootForce = 1f;

    public override void Setup(Hand _hand, int pointCount = 10)
    {
        hand = _hand;
        points = new List<Vector3>();
        prevPoint = Transform.position;
    }

    protected override void FixedUpdate()
    {
        prevPoint = hand.Forward();
    }

    public override Vector3 GetVelocity3D()
    {
        return prevPoint * shootForce;
    }
}
