using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public abstract class EzAnimPlayer : MonoBehaviour
{
    [SerializeField] protected float timeScale = 1f;

    protected float DeltaTime { get { return Time.deltaTime * TimeScale; } }

    public float TimeScale { get => timeScale; set => timeScale = value; }
    public float Timer { get => timer; protected set => timer = value; }

    [SerializeField] protected List<EzComponent> components = new List<EzComponent>();
    private float timer = 0f;

    void OnValidate()
    {
        if (!this.enabled)
        { this.enabled = true; }
    }
    void Start()
    {
        this.enabled = false;
    }

    protected void UpdateEvents()
    {
        foreach (EzComponent c in components)
        { c.Tick(Timer, DeltaTime); }
    }
}