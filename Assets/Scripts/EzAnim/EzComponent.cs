using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public abstract class EzComponent : MonoBehaviour
{
    public abstract void Tick(float timeStep, float deltaTime);
    public abstract void AddAction(EzAction action);
    public abstract void Clear();
}
