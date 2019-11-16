using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusInput : HandInput
{
    private float curTrig = 0f;
    private float prevTrig = 0f;

    public override bool ReleaseGrab()
    {
        if (curTrig <= 0.4f && prevTrig > 0.4f) { return true; }
        return false;
    }

    public override bool TryGrab()
    {
        if (curTrig >= 0.6f /*&& prevTrig < 0.6f*/) { return true; }
        return false;
    }

    public override bool SwitchMode()
    {
        switch (HandType)
        {
            case HandType.LeftHand:
                if (OVRInput.GetDown(OVRInput.RawButton.A)) { return true; }
                if (OVRInput.GetDown(OVRInput.RawButton.B)) { return true; }
                break;
            case HandType.RightHand:
                if (OVRInput.GetDown(OVRInput.RawButton.X)) { return true; }
                if (OVRInput.GetDown(OVRInput.RawButton.Y)) { return true; }
                break;
        }
        return false;
    }

    public override void UpdateInput()
    {
        prevTrig = curTrig;
        switch (HandType)
        {
            case HandType.LeftHand:
                curTrig = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
                break;
            case HandType.RightHand:
                curTrig = OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger);
                break;
        }
    }

    public override bool ViewScreen()
    {
        return false;
    }
}
