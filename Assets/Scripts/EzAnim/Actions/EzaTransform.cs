using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransformAction", menuName = "Ez/TransformAction", order = 11)]
public class EzaTransform : EzAction
{
    [SerializeField] private List<EzdVector3> positionActions = new List<EzdVector3>();
    [SerializeField] private List<EzdVector3> rotationActions = new List<EzdVector3>();
    [SerializeField] private List<EzdVector3> scaleActions = new List<EzdVector3>();

    public void ApplyActions(EzcTransform t, float timeStep, float deltaTime)
    {
        foreach (EzdVector3 a in positionActions)
        {
            t.PositionChange += a.GetChange(t.transform.localPosition, timeStep, deltaTime);
        }
        foreach (EzdVector3 a in rotationActions)
        {
            t.RotationChange += a.GetChange(t.transform.localEulerAngles, timeStep, deltaTime);
        }
        foreach (EzdVector3 a in scaleActions)
        {
            t.ScaleChange += a.GetChange(t.transform.localScale, timeStep, deltaTime);
        }
    }
}
