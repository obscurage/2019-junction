
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamColChange : ColorChanger
{
    [SerializeField] private Camera camera;
    public override Color GetCurrentColor()
    {
      return  camera.backgroundColor;
    }

    public override void SetColor(Color newColor)
    {
        camera.backgroundColor = newColor;
    }
}
