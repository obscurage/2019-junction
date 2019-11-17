using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcInputs : HandInput
{
    public override bool ReleaseGrab()
    {
        switch (HandType)
        {
            case HandType.LeftHand:
                if(Input.GetKeyUp(KeyCode.Mouse0)) { return true; }
                break;
            case HandType.RightHand:
                if (Input.GetKeyUp(KeyCode.Mouse1)) { return true; }
                break;
        }
        return false;
    }

    public override bool TryGrab()
    {
        switch (HandType)
        {
            case HandType.LeftHand:
                if (Input.GetKey(KeyCode.Mouse0)) { return true; }
                break;
            case HandType.RightHand:
                if (Input.GetKey(KeyCode.Mouse1)) { return true; }
                break;
        }
        return false;
    }

    public override bool SwitchMode()
    {
        if(hand.HandType == HandType.LeftHand) { return false; }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        { return true; }
        return false;
    }

    public override void UpdateInput()
    {
    }

    public override bool ViewScreen()
    {
        if (hand.HandType == HandType.LeftHand) { return false; }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        { return true; }
        return false;
    }
}
