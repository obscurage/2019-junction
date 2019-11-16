using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grabbable))]
[RequireComponent(typeof(Gravitable))]

public class Meteorite : MonoBehaviour
{
    [SerializeField] private MeteoriteType meteoriteType;
    [SerializeField] private float power;

    [HideInInspector] [SerializeField] private Grabbable grabbable;
    [HideInInspector] [SerializeField] private Gravitable gravitable;

    private bool isThrown = false;
    private bool isDestroyed = false;

    public bool IsDestroyed { get => isDestroyed; }
    public void SetThrown() { isThrown = true; }

    void OnValidate()
    {
        if (grabbable is null) { grabbable = GetComponent<Grabbable>(); }
        if (gravitable is null) { gravitable = GetComponent<Gravitable>(); }
        gameObject.layer = 9; // 9 = meteorite layer
    }

    void OnCollisionEnter(Collision col)
    {
        if (!isThrown) { return; }
        if (IsDestroyed) { return; }


    }

}

public enum MeteoriteType
{
    normal = 0,
    orange = 1,
    purple = 2,
}