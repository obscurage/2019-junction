using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EzPlayer : EzAnimPlayer
{
    void LateUpdate()
    {
        Timer += DeltaTime;
        if (Timer >= 1)
        {
            Timer = 1f;
            this.enabled = false;
        }

        UpdateEvents();

        if (!this.enabled)
        {

            foreach (EzComponent component in components)
            { component.Clear(); }
        }
    }

    public void Play(EzEvent ezEvent)
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
