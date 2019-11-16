using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Events;

public static class ExtensionsEvents
{
    /// <summary>
    /// EDITOR ONLY
    /// </summary>
    public static void OnValidateOnlyAddEvent(this UnityEvent e, UnityAction action)
    {
        UnityEventTools.RemovePersistentListener(e, action);
        UnityEventTools.AddPersistentListener(e, action);
    }
    public static void OnValidateOnlyAddEvent(this UnityEventBool e, UnityAction<bool> action)
    {
        UnityEventTools.RemovePersistentListener(e, action);
        UnityEventTools.AddPersistentListener(e, action);
    }
}

[System.Serializable]
public class UnityEventFloat : UnityEvent<float> { }
[System.Serializable]
public class UnityEventInt : UnityEvent<int> { }
[System.Serializable]
public class UnityEventBool : UnityEvent<bool> { }
[System.Serializable]
public class UnityEventString : UnityEvent<string> { }
[System.Serializable]
public class UnityEventGameObject : UnityEvent<GameObject> { }
[System.Serializable]
public class UnityEventTransform : UnityEvent<Transform> { }
[System.Serializable]
public class UnityEventVector3 : UnityEvent<Vector3> { }
[System.Serializable]
public class UnityEventVector2 : UnityEvent<Vector2> { }

