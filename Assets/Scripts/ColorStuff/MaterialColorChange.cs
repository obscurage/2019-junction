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
        if(renderer is null) { return new Color(0,0,0,0); }
        return renderer.material.GetColor("_Color"); 
    }

    public override void SetColor(Color newColor)
    {
        if (renderer is null) { return; }
        renderer.material.SetColor("_Color", newColor); 
    }
}