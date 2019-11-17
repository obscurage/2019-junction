using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : ModeChanger
{
    [SerializeField] private List<ModeLayerMaskPair> modeLayers = new List<ModeLayerMaskPair>();

    public override void ModeChange(ViewMode mode)
    {
        LayerMask mask = new LayerMask();
        foreach (ModeLayerMaskPair lm in modeLayers)
        {
            if (lm.Mode == mode)
            { mask = lm.Mask; break; }

        }
        foreach (Camera cam in Player.instance.Cameras)
        {
            cam.cullingMask = mask.value;
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
