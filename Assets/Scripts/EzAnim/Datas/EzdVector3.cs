using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EzdVector3 : ScriptableObject
{
    [SerializeField] private AnimationCurve x;
    [SerializeField] private AnimationCurve y;
    [SerializeField] private AnimationCurve z;

    public EzdVector3()
    {
        x = new AnimationCurve(new Keyframe(0, 0));
        x.AddKey(1, 0);

        y = new AnimationCurve(new Keyframe(0, 0));
        y.AddKey(1, 0);

        z = new AnimationCurve(new Keyframe(0, 0));
        z.AddKey(1, 0);
    }

    void OnValidate()
    {
        x.postWrapMode = WrapMode.Loop;
        x.preWrapMode = WrapMode.Loop;
        y.postWrapMode = WrapMode.Loop;
        y.preWrapMode = WrapMode.Loop;
        z.postWrapMode = WrapMode.Loop;
        z.preWrapMode = WrapMode.Loop;
    }

    public Vector3 Evaluate(float timeStep)
    {
        return new Vector3(x.Evaluate(timeStep), y.Evaluate(timeStep), z.Evaluate(timeStep));
    }

    public Vector3 EvaluateDelta(float timeStep, float deltaTime)
    {
        return Evaluate(timeStep) - Evaluate(timeStep - deltaTime);
    }

    public abstract Vector3 GetChange(Vector3 input, float timeStep, float deltaTime);
}
