using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorChange : ColorChanger
{
    [SerializeField] private MeshRenderer renderer = null;

    private void OnValidate()
    {
        if(renderer is null)
        { renderer = GetComponent<MeshRenderer>(); }
    }

    public override Color GetCurrentColor()
    {
        return renderer.material.GetColor("_Color");
    }

    public override void SetColor(Color newColor)
    {
        renderer.material.SetColor("_Color", newColor);
    }
}