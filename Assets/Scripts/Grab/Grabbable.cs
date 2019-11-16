using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Gravitable))]
[RequireComponent(typeof(SphereCollider))]

public class Grabbable : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Gravitable gravitable;
    [SerializeField] private Collider collider;

    [SerializeField] private UnityEvent onGrabEvent;
    [SerializeField] private UnityEvent onReleaseEvent;

    public UnityEvent OnReleaseEvent { get => onReleaseEvent; set => onReleaseEvent = value; }
    public UnityEvent OnGrabEvent { get => onGrabEvent; set => onGrabEvent = value; }
    public Hand Grabber { get => grabber; private set => grabber = value; }

    private Hand grabber = null;

    void OnValidate()
    {
        if (rb is null) { rb = GetComponent<Rigidbody>(); }
        if (gravitable is null) { gravitable = GetComponent<Gravitable>(); }
        if (collider is null) { collider = GetComponent<Collider>(); }
        collider.isTrigger = false;
    }

    public void Grab(Hand hand)
    {
        Grabber = hand;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        gravitable.Freeze();
        Parentize(true);
        OnGrabEvent.Invoke();

        gameObject.AddComponent<Centerize>().Play(0.5f);
    }
    public void Release(Hand hand)
    {
        Parentize(false);
        rb.constraints = RigidbodyConstraints.None;
        AddForce();
        gravitable.ApplyGravitalForce = true;
        OnReleaseEvent.Invoke();
        Grabber = null;
    }

    public bool CanPick()
    {
        if (Grabber is null)
        { return true; }
        return false;
    }

    protected void AddForce()
    {
        gravitable.AddForce(Grabber.VelocityTracker.GetVelocity3D());
    }

    protected void Parentize(bool state)
    {
        if (state && Grabber != null)
        {
            transform.parent = Grabber.transform;
        }
        else
        {
            transform.parent = null;
        }
    }
}
