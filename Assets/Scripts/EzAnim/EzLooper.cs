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
        Timer += DeltaTime;
        if (Timer >= 1)
        {
            Timer -= 1f;
        }

        UpdateEvents();
    }

    private void Play(EzEvent ezEvent)
    {
        this.enabled = true;
        Timer = 0f;
        foreach (EzComponent component in components)
        {
            foreach (EzAction action in ezEvent.Actions)
            { component.AddAction(action); }
        }
    }
}
