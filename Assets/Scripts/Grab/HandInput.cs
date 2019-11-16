using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hand))]
public abstract class HandInput : MonoBehaviour
{
    [HideInInspector] [SerializeField] protected Hand hand;
    protected HandType HandType { get => hand.HandType; }

    void OnValidate()
    {
        if (hand is null) { hand = GetComponent<Hand>(); }
    }

    public abstract void UpdateInput();

    public abstract bool StartGrab();
    public abstract bool ReleaseGrab();

    public abstract bool SwitchMode();

    public abstract bool ViewScreen();
}
