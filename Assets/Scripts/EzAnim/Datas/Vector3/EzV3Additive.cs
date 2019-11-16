using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector3Additive", menuName = "Ez/Vector3 Additive", order = 101)]
public class EzV3Additive : EzdVector3
{
    public override Vector3 GetChange(Vector3 input, float timeStep, float deltaTime)
    {
        return EvaluateDelta(timeStep, deltaTime);
    }
}