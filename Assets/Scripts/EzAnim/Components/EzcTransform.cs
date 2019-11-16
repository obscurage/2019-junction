using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EzcTransform : EzComponent
{
    private List<EzaTransform> actions = new List<EzaTransform>();

    public override void AddAction(EzAction action)
    {
        if (action is EzaTransform a)
        { actions.Add(a); }
    }

    public Vector3 PositionChange { get; set; }
    public Vector3 ScaleChange { get; set; }
    public Vector3 RotationChange { get; set; }

    public override void Tick(float timeStep, float deltaTime)
    {
        PositionChange = new Vector3();
        ScaleChange = new Vector3();
        RotationChange = new Vector3();

        foreach (EzaTransform actions in actions)
        { actions.ApplyActions(this, timeStep, deltaTime); }

        transform.localPosition += PositionChange;
        transform.localEulerAngles += RotationChange;
        transform.localScale += ScaleChange;
    }

    public override void Clear()
    {
        actions = new List<EzaTransform>();
    }
}
