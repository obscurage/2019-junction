
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum HandType
{
    LeftHand = 0,
    RightHand = 1,
}

public class Hand : MonoBehaviour
{
    [SerializeField] private HandInput input;

    [SerializeField] private HandType handType;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private LayerMask grabMask;

    [SerializeField] private UnityEventBool OnGrabStart = new UnityEventBool();
    [SerializeField] private UnityEventBool OnGrabEnd = new UnityEventBool();

    private HandVelocityTracker velocityTracker;
    private Grabbable grabbed = null;

    public HandVelocityTracker VelocityTracker { get => velocityTracker; private set => velocityTracker = value; }
    public HandType HandType { get => handType; }

    void OnValidate()
    {
        if (input is null) { input = GetComponent<HandInput>(); }
    }

    void Start()
    {
        VelocityTracker = gameObject.AddComponent<HandVelocityTracker>();
        VelocityTracker.Setup(this);
    }

    void Update()
    {
        input.UpdateInput();

        if (RayInput())
        {
            bool state = TryPick();
            OnGrabStart.Invoke(state);
        }
        if (ReleaseInput())
        {
            bool state = Release();
            OnGrabStart.Invoke(state);
        }

        if (input.ViewScreen())
        {
            ViewScreen();
        }
        if (input.SwitchMode())
        {
            SwitchMode();
        }

        prevTrig = curTrig;
    }

    private float curTrig = 0f;
    private float prevTrig = 0f;

    private bool RayInput()
    {
        if (grabbed is null)
        {
            if (input.StartGrab()) { return true; }
        }
        return false;
    }
    private bool ReleaseInput()
    {
        if (grabbed != null)
        {
            if (input.ReleaseGrab()) { return true; }
        }
        return false;
    }

    private Grabbable Ray()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Forward() * rayDistance, Color.red, 1f);
        if (Physics.Raycast(transform.position, Forward(), out hit, rayDistance, grabMask))
        {
            return hit.transform.GetComponent<Grabbable>();
        }
        return null;
    }

    private Vector3 Forward()
    {
        return transform.forward;
    }

    private bool TryPick()
    {
        Grabbable target = Ray();

        if (target is null) { return false; }
        if (!target.CanPick()) { return false; }

        grabbed = target;
        grabbed.Grab(this);
        return true;
    }

    private bool Release()
    {
        if (grabbed is null) { return false; }
        grabbed.Release(this);
        grabbed = null;
        return true;
    }

    private void SwitchMode()
    {

    }

    private void ViewScreen()
    {

    }
}