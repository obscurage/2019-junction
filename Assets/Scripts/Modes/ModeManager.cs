
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class ModeManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.ModeManager = this;
    }
    private void Start()
    {
        currentMode = ViewMode.orange;
        SetMode(ViewMode.purple);
    }

    [SerializeField] private ViewMode currentMode = ViewMode.purple;
    private List<ModeChanger> changers = new List<ModeChanger>();

    public ViewMode CurrentMode { get => currentMode; private set => currentMode = value; }
    public List<ModeChanger> Changers { get => changers; set => changers = value; }

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
        if (mode == CurrentMode) { return; }
        CurrentMode = mode;
        AudioManager.instance.ModeChange(mode);
        foreach (ModeChanger changers in Changers)
        { changers.ModeChange(CurrentMode); }
    }
}

public enum ViewMode
{
    orange = 1,
    purple = 2,
}