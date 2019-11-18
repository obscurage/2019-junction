using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffColChange : ColorChanger
{
    [SerializeField] private ParticleSystem particle;

    private void OnValidate()
    {
        if (particle is null)
        { particle = GetComponent<ParticleSystem>(); }
    }

    public override Color GetCurrentColor()
    {
        ParticleSystem.MainModule settings = particle.main;
        return settings.startColor.color;
    }

    public override void SetColor(Color newColor)
    {
        ParticleSystem.MainModule settings = particle.main;
        settings.startColor = new ParticleSystem.MinMaxGradient(newColor);
    }
}
