using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class LifeManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.LifeManager = this;
    }
}
