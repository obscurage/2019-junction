using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorChanger : MonoBehaviour
{
    private float duration = 1f;
    private float timer = 0f;

    private Color startColor;
    private Color colorChange;
    private Color currentColor;

    public void ChangeTo(Color targetColor, float _duration = 1f)
    {
        duration = _duration;
        timer = 0f;
        startColor = GetCurrentColor();

        colorChange = new Color(
        targetColor.r - startColor.r,
        targetColor.g - startColor.g,
        targetColor.b - startColor.b,
        targetColor.a - startColor.a);

        currentColor = startColor;
        this.enabled = true;
    }

    void Update()
    {
        timer += Time.deltaTime / duration;
        if (timer >= 1f)
        { timer = 1f; this.enabled = false; }

        currentColor = new Color(
        startColor.r + (colorChange.r * timer),
        startColor.g + (colorChange.g * timer),
        startColor.b + (colorChange.b * timer),
        startColor.a + (colorChange.a * timer));

        SetColor(currentColor);
    }

    public abstract Color GetCurrentColor();
    public abstract void SetColor(Color newColor);

}