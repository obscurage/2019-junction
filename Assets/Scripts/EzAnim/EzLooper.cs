using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EzLooper : EzAnimPlayer
{
    [SerializeField] private EzEvent loopingEvent;

    void Start()
    {
        Play(loopingEvent);
    }


    void LateUpdate()
    {
        timer += DeltaTime;
        if (timer >= 1)
        {
            timer -= 1f;
        }

        UpdateEvents();
    }

    private void Play(EzEvent ezEvent)
    {
        this.enabled = true;
        timer = 0f;
        foreach (EzComponent component in components)
        {
            foreach (EzAction action in ezEvent.Actions)
            { component.AddAction(action); }
        }
    }
}
