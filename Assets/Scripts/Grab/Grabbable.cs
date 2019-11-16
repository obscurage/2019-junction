using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grabbable : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private UnityEvent onGrabEvent;
    [SerializeField] private UnityEvent onReleaseEvent;

    public UnityEvent OnReleaseEvent { get => onReleaseEvent; set => onReleaseEvent = value; }
    public UnityEvent OnGrabEvent { get => onGrabEvent; set => onGrabEvent = value; }
    public Hand Grabber { get => grabber; private set => grabber = value; }

    private Hand grabber = null;

    void OnValidate()
    {
        if(rb is null) { rb = GetComponent<Rigidbody>(); }
    }

    public void Grab(Hand hand)
    {
        Grabber = hand;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Parentize(true);
        OnGrabEvent.Invoke();

        gameObject.AddComponent<Centerize>().Play(0.5f);
    }
    public void Release(Hand hand)
    {
        Parentize(false);
        rb.constraints = RigidbodyConstraints.None;
        AddForce();
        OnReleaseEvent.Invoke();
        Grabber = null;
    }

    public bool CanPick()
    {
        if(Grabber is null)
        { return true; }
        return false;
    }

    protected void AddForce()
    {
        rb.AddForce(Grabber.VelocityTracker.GetVelocity3D(), ForceMode.Impulse);
    }

    protected void Parentize(bool state)
    {
        if(state && Grabber != null)
        {
            transform.parent = Grabber.transform;
        }
        else
        {
            transform.parent = null;
        }
    }
}
