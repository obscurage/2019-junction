using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grabbable))]
[RequireComponent(typeof(Gravitable))]

public class Meteorite : MonoBehaviour
{
    [SerializeField] private MeteoriteType meteoriteType;
    [SerializeField] private float power;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private AudioClip neglateSound;
    [SerializeField] private AudioClip fuseSound;

    [HideInInspector] [SerializeField] private Grabbable grabbable;
    [HideInInspector] [SerializeField] private Gravitable gravitable;

    [SerializeField] private AudioSource oneShot;
    [SerializeField] private AudioClip fuseSound;
    [SerializeField] private AudioClip conflictSound;

    private bool isThrown = false;
    private bool isDestroyed = false;

    public bool IsDestroyed { get => isDestroyed; }
    public MeteoriteType MeteoriteType { get => meteoriteType; }
    public Gravitable Gravitable { get => gravitable; private set => gravitable = value; }

    public void SetThrown() { isThrown = true; }

    void OnValidate()
    {
        if (grabbable is null) { grabbable = GetComponent<Grabbable>(); }
        if (Gravitable is null) { Gravitable = GetComponent<Gravitable>(); }
        gameObject.layer = 9; // 9 = meteorite layer

        grabbable.OnReleaseEvent.OnValidateOnlyAddEvent(SetThrown);
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, Player.instance.Head.position) > 1000f)
        {
            Destroy(gameObject);
        }
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
                power += meteorite.power + 1;
                OnFuse();
                break;
            case DestroyType.suicide:
                break;
        }
    }

    private void OnFuse()
    {
        transform.localScale = Vector3.one * (1 + (power * 0.3f));
        GetComponentInChildren<AudioSource>().PlayOneShot(fuseSound);

    }

    private void OnConflict()
    {
        GetComponentInChildren<AudioSource>().PlayOneShot(neglateSound);
    }

    private void DestroyMeteorite(DestroyType dType)
    {
        GetComponentInChildren<AudioSource>().PlayOneShot(destroySound);
        isDestroyed = true;
        destroyEffect.SpawnParticleEffect(transform.position);
        Destroy(gameObject);
    }

    private DestroyType GetDestroyType(Meteorite other)
    {
        if (other.MeteoriteType != MeteoriteType.normal && other.MeteoriteType != MeteoriteType && MeteoriteType != MeteoriteType.normal)
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