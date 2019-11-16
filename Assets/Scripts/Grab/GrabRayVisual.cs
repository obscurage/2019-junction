using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Hand))]
public class GrabRayVisual : MonoBehaviour
{
    [HideInInspector] [SerializeField] private Hand hand;
    [HideInInspector] [SerializeField] private LineRenderer rend;
    [SerializeField] private AudioSource looping;
    [SerializeField] private AudioSource oneShot;
    [Space(10)]
    [SerializeField] private float fluctuation = 1f;
    [SerializeField] private int points = 10;
    [Space(10)]
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve curve;
    [Space(20)]
    [Range(0.00f, 1.00f)]
    [SerializeField]
    private float grabSoundTime = 0.6f;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip grabSound;
    [SerializeField] private AudioClip throwSound;
    [Space(10)]
    [SerializeField] private AudioClip suckSound;
    [SerializeField] private AudioClip holdSound;

    private float timer = 0f;
    private float distance = 1f;
    private float currentDistance;

    void OnValidate()
    {
        if (rend is null) { rend = GetComponent<LineRenderer>(); }
        if (hand is null) { hand = GetComponent<Hand>(); }
        if (points < 2) { points = 2; }
        if (points != rend.positionCount)
        {
            rend.positionCount = points;
            for (int i = 0; i < points; i++)
            {
                rend.SetPosition(i, transform.position);
            }
        }
        Enabled(false);

        hand.OnGrabStart.OnValidateOnlyAddEvent(Play);
        hand.OnGrabEnd.OnValidateOnlyAddEvent(Stop);
    }


    void Update()
    {
        timer += Time.deltaTime / duration;
        if (hand.Grabbed != null && oneShot.clip != grabSound && timer >= grabSoundTime)
        { oneShot.PlayOneShot(grabSound); }
        if (timer >= 1f)
        {
            hand.OuterRayAbort = true;
            timer = 1f;
            if (hand.Grabbed) { 
                looping.clip = holdSound;
                looping.Play();
            }
            this.enabled = false;
            rend.enabled = false;
        }
        currentDistance = curve.Evaluate(timer) * distance;

        rend.SetPosition(0, transform.position);
        for (int i = 1; i < rend.positionCount; i++)
        {
            rend.SetPosition(i, (transform.position + (((hand.Forward() * i) / (rend.positionCount - 1)) * currentDistance)).FluctuatePosition(fluctuation));
        }
    }

    public void Play(bool grabbed)
    {
        rend.SetPosition(0, transform.position);
        for (int i = 1; i < rend.positionCount; i++)
        {
            rend.SetPosition(i, transform.position);
        }

        distance = hand.RayDistance;
        timer = 0f;
        oneShot.PlayOneShot(shootSound);
        Enabled(true);
    }

    public void Stop(bool released)
    {
        if (released)
        { oneShot.PlayOneShot(throwSound); }
        looping.Stop();
        Enabled(false);
    }

    private void Enabled(bool state)
    {
        this.enabled = state;
        rend.enabled = state;
    }
}