using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionsPrefab
{
    public static void SpawnParticleEffect(this GameObject go, Transform location)
    {
        GameManager.instance.SpawnParticleEffect(go, location.position);
    }
    public static void SpawnParticleEffect(this GameObject go, Vector3 location)
    {
        GameManager.instance.SpawnParticleEffect(go, location);
    }
}
