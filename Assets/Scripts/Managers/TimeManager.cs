using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class TimeManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.TimeManager = this;
    }
}
