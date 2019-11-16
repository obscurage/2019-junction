using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Hand))]
public class GrabRayVisual : MonoBehaviour
{
    [HideInInspector] [SerializeField] private Hand hand;
    [HideInInspector] [SerializeField] private LineRenderer rend;
    [SerializeField] private float duration = 1f;
    [SerializeField] private int points = 10;
    [SerializeField] private AnimationCurve curve;

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
        this.enabled = false;
        rend.enabled = false;
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        { timer = 1f; this.enabled = false; rend.enabled = false; }
        currentDistance = curve.Evaluate(timer) * distance;

        rend.SetPosition(0, transform.position);
        for (int i = 1; i < rend.positionCount; i++)
        {
            rend.SetPosition(i, (transform.position + (((hand.Forward() * i) / (rend.positionCount - 1)) * currentDistance)).FluctuatePosition(0.2f));
        }
    }

    public void Play(float _distance)
    {
        distance = _distance;
        timer = 0f;
        this.enabled = true;
        rend.enabled = true;
    }
}