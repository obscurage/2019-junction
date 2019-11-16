
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum HandType
{
    LeftHand = 0,
    RightHand = 1,
}

[RequireComponent(typeof(HandVelocityTracker))]
public class Hand : MonoBehaviour
{
    [SerializeField] private HandInput input;
    [SerializeField] private Transform grabPoint;

    [SerializeField] private HandType handType;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private LayerMask grabMask;

    [SerializeField] private UnityEvent onShootStart = new UnityEvent();
    [SerializeField] private UnityEventBool onGrabStart = new UnityEventBool();
    [SerializeField] private UnityEventBool onGrabEnd = new UnityEventBool();

    private HandVelocityTracker velocityTracker;
    private Grabbable grabbed = null;

    public HandVelocityTracker VelocityTracker { get => velocityTracker; private set => velocityTracker = value; }
    public HandType HandType { get => handType; }
    public UnityEventBool OnGrabStart { get => onGrabStart; set => onGrabStart = value; }
    public UnityEventBool OnGrabEnd { get => onGrabEnd; set => onGrabEnd = value; }
    public float RayDistance { get => rayDistance; }
    public Transform GrabPoint { get => grabPoint; }
    public UnityEvent OnShootStart { get => onShootStart; set => onShootStart = value; }

    public bool OuterRayAbort { get; set; } = false;
    private bool shooting = false;
    public Grabbable Grabbed { get => grabbed; private set => grabbed = value; }

    void OnValidate()
    {
        if (input is null) { input = GetComponent<HandInput>(); }
        if (VelocityTracker is null) { VelocityTracker = GetComponent<HandVelocityTracker>(); }
    }

    void Start()
    {
        VelocityTracker.Setup(this);
    }

    void Update()
    {
        input.UpdateInput();

        if (RayInput())
        {
            bool state = TryPick();
            if (!shooting)
            {
                OnGrabStart.Invoke(state);
                OuterRayAbort = false;
            }
            shooting = true;
        }
        if (ReleaseInput())
        {
            bool state = Release();
            OnGrabEnd.Invoke(state);
            shooting = false;
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
        if (input.TryGrab()) { return true; }
        return false;
    }
    private bool ReleaseInput()
    {

        if (input.ReleaseGrab()) { return true; }
        return false;
    }

    private Grabbable Ray()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Forward() * RayDistance, Color.red, 1f);
        if (Physics.Raycast(transform.position, Forward(), out hit, RayDistance, grabMask))
        {
            return hit.transform.GetComponent<Grabbable>();
        }
        return null;
    }

    public Vector3 Forward()
    {
        return transform.forward;
    }

    private bool TryPick()
    {
        if (OuterRayAbort) { return false; }
        if (Grabbed != null) { return false; }
        Grabbable target = Ray();

        if (target is null) { return false; }
        if (!target.CanPick()) { return false; }

        Grabbed = target;
        Grabbed.Grab(this);
        return true;
    }

    private bool Release()
    {
        if (Grabbed is null) { return false; }
        Grabbed.Release(this);
        Grabbed = null;
        return true;
    }

    private void SwitchMode()
    {
        GameManager.instance.ModeManager.SwitchMode();
    }

    private void ViewScreen()
    {

    }
}