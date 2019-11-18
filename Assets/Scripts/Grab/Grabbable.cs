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
    [SerializeField] private UnityEvent onAbortReleaseEvent;

    public UnityEvent OnReleaseEvent { get => onReleaseEvent; set => onReleaseEvent = value; }
    public UnityEvent OnGrabEvent { get => onGrabEvent; set => onGrabEvent = value; }
    public UnityEvent OnAbortReleaseEvent { get => onAbortReleaseEvent; set => onAbortReleaseEvent = value; }
    public Hand Grabber { get => grabber; private set => grabber = value; }

    public Centerize Centerize { get; private set; }

    private Hand grabber = null;

    void OnValidate()
    {
        if (rb is null) { rb = GetComponent<Rigidbody>(); }
        if (gravitable is null) { gravitable = GetComponent<Gravitable>(); }
        if (collider is null) { collider = GetComponent<Collider>(); }
        if (collider.isTrigger != false)
        { collider.isTrigger = false; }
    }

    public void Grab(Hand hand)
    {
        Grabber = hand;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        gravitable.Freeze();
        Parentize(true);
        OnGrabEvent.Invoke();
        collider.enabled = false;
        Centerize = gameObject.AddComponent<Centerize>();
        Centerize.Play(0.5f);
    }
    public void Release(Hand hand)
    {
        if (Centerize != null)
        {
            Centerize.Hand = hand;
            Centerize.OnCenterEnd.AddListener(ReleaseActivity);
        }
        else
        { ReleaseActivity(hand); }
    }

    private void ReleaseActivity(Hand hand)
    {
        Parentize(false);
        rb.constraints = RigidbodyConstraints.None;
        AddForce();
        gravitable.ApplyGravitalForce = true;
        collider.enabled = true;
        OnReleaseEvent.Invoke();
        Grabber = null;
    }

    public void AbortRelease(Hand hand)
    {
        return;
        Debug.Log("aborted");
        Grabber = hand;
        if (Centerize != null)
        { Centerize.Killadsaf(); }
        gravitable.Freeze();
        AddForceTowardsHand(-0.25f);
        gravitable.ApplyGravitalForce = true;
        OnAbortReleaseEvent.Invoke();
        Grabber = null;
    }

    public bool CanPick()
    {
        if (Grabber is null)
        { return true; }
        return false;
    }

    protected void AddForce(float multiplier = 1f)
    {
        gravitable.AddForce(Grabber.VelocityTracker.GetVelocity3D() * multiplier);
    }
    private void AddForceTowardsHand(float multiplier = 1f)
    {
        gravitable.AddForce(Grabber.VelocityTracker.GetVelocity3D().magnitude * (grabber.transform.position - transform.position) * multiplier);
    }

    protected void Parentize(bool state)
    {
        if (state && Grabber != null)
        {
            Vector3 pos = transform.position;
            transform.parent = Grabber.GrabPoint;
            transform.position = pos;
        }
        else
        {
            transform.parent = null;
        }
    }
}
