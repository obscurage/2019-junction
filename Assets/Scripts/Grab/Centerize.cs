using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centerize : MonoBehaviour
{
    private Vector3 startLocation = new Vector3();
    private float state = 1f;
    private float duration = 1f;

    public void Play(float _duration)
    {
        duration = _duration;
        startLocation = transform.localPosition;
        state = 1f;
    }

    void Update()
    {
        state -= Time.deltaTime / duration;
        if (state <= 0)
        {
            state = 0;
            Destroy(this);
        }
        transform.localPosition = startLocation * state;
    }
}
