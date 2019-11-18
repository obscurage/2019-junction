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
    [HideInInspector] [SerializeField] private GravitySource gravitySource;

    [SerializeField] private bool updateMeteorite = false;
    [SerializeField] List<ModeChanger> changers = new List<ModeChanger>();

    private bool isThrown = false;
    private bool isDestroyed = false;

    public bool IsDestroyed { get => isDestroyed; }
    public MeteoriteType MeteoriteType { get => meteoriteType; }
    public Gravitable Gravitable { get => gravitable; private set => gravitable = value; }
    public GravitySource GravitySource { get => gravitySource; private set => gravitySource = value; }

    public void SetThrown() { isThrown = true; }

    void OnValidate()
    {
        if (grabbable is null) { grabbable = GetComponent<Grabbable>(); }
        if (Gravitable is null) { Gravitable = GetComponent<Gravitable>(); }
        if (GravitySource is null) { GravitySource = GetComponentInChildren<GravitySource>(); }
        if (gameObject.layer != 9)
        { gameObject.layer = 9; } // 9 = meteorite layer

        if (updateMeteorite)
        {
            updateMeteorite = false;
            changers = new List<ModeChanger>(GetComponentsInChildren<ModeChanger>());
        }
        grabbable.OnReleaseEvent.OnValidateOnlyAddEvent(SetThrown);
    }

    private bool collided = false;

    private void Start()
    {
        foreach(ModeChanger mc in changers)
        { mc.ModeChange(GameManager.instance.ModeManager.CurrentMode); }
    }

    void FixedUpdate()
    {
        collided = false;
        if (Vector3.Distance(transform.position, Player.instance.Head.position) > 1000f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(collided) { return; }
        if (IsDestroyed) { return; }
        collided = true;

        Meteorite other = col.gameObject.GetComponent<Meteorite>();
        if (other is null) { return; }

        other.HitBy(this);

        if (MeteoriteType == MeteoriteType.normal) { DestroyMeteorite(); }
        else if (other.meteoriteType == MeteoriteType.normal && other.meteoriteType != MeteoriteType) { return; }
        else
        {
            DestroyMeteorite();
        }
    }

    public void HitBy(Meteorite meteorite)
    {
        if (collided) { return; }
        collided = true;

        if(MeteoriteType == MeteoriteType.normal) { DestroyMeteorite(); }
        else if(meteorite.meteoriteType == MeteoriteType.normal && meteorite.meteoriteType != MeteoriteType) { return; }
        else
        {
            if(MeteoriteType == meteorite.MeteoriteType)
            {
                OnFuse(meteorite);
            }
            else
            {
                OnConflict();
                DestroyMeteorite();
            }
        }
        return;
    }

    private void OnFuse(Meteorite meteorite)
    {
        power += meteorite.power;
        transform.localScale = Vector3.one * (1 + (power * 0.3f));
        GetComponentInChildren<AudioSource>().PlayOneShot(fuseSound);

    }

    private void OnConflict()
    {
        GetComponentInChildren<AudioSource>().PlayOneShot(neglateSound);
    }

    private void DestroyMeteorite()
    {
        foreach (ModeChanger c in changers)
        { GameManager.instance.ModeManager.Changers.Remove(c); }

        GravitySource.WipeTargets();
        GetComponentInChildren<AudioSource>().PlayOneShot(destroySound);
        isDestroyed = true;
        destroyEffect.SpawnParticleEffect(transform.position);
        Destroy(gameObject);
    }

    private DestroyType GetDestroyType(Meteorite other)
    {
        if (
            other.MeteoriteType != MeteoriteType.normal
            && MeteoriteType != MeteoriteType.normal
            && other.MeteoriteType != MeteoriteType)
        { return DestroyType.conflict; }

        if (MeteoriteType != MeteoriteType.normal 
            && other.MeteoriteType == MeteoriteType)
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