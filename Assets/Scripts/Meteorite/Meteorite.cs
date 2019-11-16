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
    public MeteoriteType MeteoriteType { get => meteoriteType; }

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

        Meteorite other = col.gameObject.GetComponent<Meteorite>();
        if (other is null) { return; }

        DestroyType dType = GetDestroyType(other);

        other.HitBy(this, dType);

        DestroyMeteorite(dType);
    }

    public void HitBy(Meteorite meteorite, DestroyType dType)
    {
        switch (dType)
        {
            case DestroyType.normal:
                DestroyMeteorite(dType);
                break;
            case DestroyType.conflict:
                OnConflict();
                DestroyMeteorite(dType);
                break;
            case DestroyType.fuse:
                power += meteorite.power;
                OnFuse();
                break;
            case DestroyType.suicide:
                break;
        }
    }

    private void OnFuse()
    {
        transform.localScale = Vector3.one * (1 + (power * 0.3f));
        // TODO fusio sound
    }

    private void OnConflict()
    {
        // TODO negate sound
    }

    private void DestroyMeteorite(DestroyType dType)
    {
        Destroy(gameObject);
    }

    private DestroyType GetDestroyType(Meteorite other)
    {
        if (other.MeteoriteType != MeteoriteType.normal && other.MeteoriteType != MeteoriteType)
        { return DestroyType.conflict; }

        if (MeteoriteType != MeteoriteType.normal && other.MeteoriteType == MeteoriteType)
        { return DestroyType.fuse; }

        if (other.MeteoriteType == MeteoriteType.normal)
        { return DestroyType.normal; }

        return DestroyType.suicide;
    }
}

public enum DestroyType
{
    normal = 0,
    conflict = 1,
    fuse = 2,
    suicide = 3,
}

public enum MeteoriteType
{
    normal = 0,
    orange = 1,
    purple = 2,
}