using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : ModeChanger
{
    [SerializeField] private LayerMask switchMode;
    [SerializeField] private List<ModeLayerMaskPair> modeLayers = new List<ModeLayerMaskPair>();
    [SerializeField] private List<Camera> cameras;

    private void OnValidate()
    {
        if (this.enabled)
        { this.enabled = false; }
    }

    public override void ModeChange(ViewMode mode)
    {
        foreach (ModeLayerMaskPair lm in modeLayers)
        {
            if (lm.Mode == mode)
            { targetMask = lm.Mask; break; }

        }
        foreach (Camera cam in cameras)
        {
            cam.cullingMask = switchMode.value;
        }

        timer = 1f;
        this.enabled = true;
    }

    private LayerMask targetMask;
    private float timer = 0f;
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            foreach (Camera cam in cameras)
            {
                cam.cullingMask = targetMask.value;
            }
            this.enabled = false;
        }
    }

    [System.Serializable]
    public class ModeLayerMaskPair
    {
        [SerializeField] private LayerMask mask;
        [SerializeField] private ViewMode mode;

        public LayerMask Mask { get => mask; }
        public ViewMode Mode { get => mode; }
    }
}
