using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centerize : MonoBehaviour
{
    private Vector3 startLocation = new Vector3();
    private float state = 1f;
    private float duration = 1f;

    public bool Enabled { get => enabled; set => enabled = value; }

    public void Play(float _duration)
    {
        duration = _duration;
        startLocation = transform.localPosition;
        state = 1f;
    }

    void Update()
    {
        if(!this.enabled) { return; }
        state -= Time.deltaTime / duration;
        if (state <= 0)
        {
            state = 0;
            Destroy(this);
        }
        transform.localPosition = startLocation * state;
    }

    public void Killadsaf()
    {
        startLocation = transform.position;
        state = 1f;
        this.enabled = false;
        Destroy(gameObject);
    }
}
