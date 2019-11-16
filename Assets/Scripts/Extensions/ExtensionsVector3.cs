using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionsVector3
{
    static Vector3 Multiply(this Vector3 v, Vector3 multiply)
    {
        return new Vector3(v.x * multiply.x, v.y * multiply.y, v.z * multiply.z);
    }
}
