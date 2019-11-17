using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxColorChanger : ColorChanger
{
    [SerializeField] private Material skybox;

    private void Awake()
    {
        skybox = RenderSettings.skybox;
    }

    public override Color GetCurrentColor()
    {
        return skybox.GetColor("_Tint");
    }

    public override void SetColor(Color newColor)
    {
        skybox.SetColor("_Tint", newColor);
    }
}