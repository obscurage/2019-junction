using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitcher : ModeChanger
{
    [SerializeField] private ColorChanger colorChanger;
    [SerializeField] private List<ColorModePair> modeColors = new List<ColorModePair>();

    void OnValidate()
    {
        if (colorChanger is null)
        {
            colorChanger = GetComponent<ColorChanger>();
        }
    }

    public override void ModeChange(ViewMode mode)
    {
        foreach (ColorModePair cm in modeColors)
        {
            if (cm.Mode == mode)
            { colorChanger.ChangeTo(cm.Color); }
        }
    }
}

[System.Serializable]
public class ColorModePair
{
    [SerializeField] private Color color;
    [SerializeField] private ViewMode mode;

    public Color Color { get => color; }
    public ViewMode Mode { get => mode; }
}