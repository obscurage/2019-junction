using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVelocityTracker : MonoBehaviour
{
    private float velocityMultiplier = 1f;
    private List<Vector3> points = new List<Vector3>();
    private Hand hand;
    private Transform Transform { get => hand.transform; }

    private Vector3 prevPoint;

    public void Setup(Hand _hand, int pointCount = 10)
    {
        hand = _hand;
        points = new List<Vector3>();
        prevPoint = Transform.position;
        for (int i = 0; i < pointCount; i++)
        { points.Add(Vector3.zero); }
    }

    void FixedUpdate()
    {
        points.Add(Transform.position - prevPoint);
        points.RemoveAt(points.Count - 1);
        prevPoint = Transform.position;
    }

    public Vector3 GetVelocity3D()
    {
        Vector3 result = Vector3.zero;
        float totalWeight = 0.00f;
        for (int i = 0; i < points.Count; i++)
        {
            result += points[i] * (points.Count - i);
            totalWeight += (points.Count - i);
        }
        return (result * velocityMultiplier) / totalWeight;
    }
}
