using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gravitable))]
public class StartForce : MonoBehaviour
{
    [SerializeField] private Vector3 forceAmount;
    [HideInInspector] [SerializeField] private Gravitable gravitable;

    void OnValidate()
    {
        if(gravitable is null) { gravitable = GetComponent<Gravitable>(); }
    }

    void Start()
    {
        gravitable.AddForce(forceAmount);
        Destroy(this);
    }
}
