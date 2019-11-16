using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModeChanger : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.ModeManager.Changers.Add(this);
    }
    public abstract void ModeChange(ViewMode mode);
}