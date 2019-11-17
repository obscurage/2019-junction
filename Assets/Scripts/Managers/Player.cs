using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] private Transform head;
    [SerializeField] private List<Camera> cameras = new List<Camera>();

    public Transform Head { get => head; }
    public List<Camera> Cameras { get => cameras; }

    void Awake()
    {
        if (instance is null)
        { instance = this; }
        else if (instance != this)
        { Destroy(gameObject); }
    }





}
