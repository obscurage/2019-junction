using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] private Transform head;

    public Transform Head { get => head; }

    void Awake()
    {
        if (instance is null)
        { instance = this; }
        else if (instance != this)
        { Destroy(gameObject); }
    }
}
