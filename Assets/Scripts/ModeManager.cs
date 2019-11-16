
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    [SerializeField] private ViewMode currentMode = ViewMode.purple;

    public ViewMode CurrentMode { get => currentMode; private set => currentMode = value; }

    public void SwitchMode()
    {
        switch (CurrentMode)
        {
            case ViewMode.orange:
                SetMode(ViewMode.purple);
                break;
            case ViewMode.purple:
                SetMode(ViewMode.orange);
                break;
        }
    }

    public void SetMode(ViewMode mode)
    {
        CurrentMode = mode;

    }
}

public enum ViewMode
{
    orange = 1,
    purple = 2,
}