using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GravitySource : MonoBehaviour
{
    [HideInInspector] [SerializeField] private SphereCollider collider;
    [SerializeField] private float gravitalForce = 1f;
    public Vector3 GetGravitalForce(Transform target)
    {
        if (transform == null) { Debug.LogWarning("idk error A"); return Vector3.zero; }
        if (target == null) { Debug.LogWarning("idk error B"); return Vector3.zero; }
        return new Vector3
        (transform.position.x - target.position.x
        , transform.position.y - target.position.y
        , transform.position.z - target.position.z)
        * (gravitalForce / Vector3.Distance(transform.position, target.position));
        ;
    }

    void OnValidate()
    {
        if (gameObject.name != "GravitalField")
        { gameObject.name = "GravitalField"; }
        if (gameObject.layer != 10)
        { gameObject.layer = 10; }// 10 = gravitalfield
        if (collider is null) { collider = GetComponent<SphereCollider>(); }
        if(collider.isTrigger != true)
        { collider.isTrigger = true; }
    }

    void OnTriggerEnter(Collider col)
    {
        col.GetComponent<Gravitable>().Gravities.Add(this);
    }

    void OnTriggerExit(Collider col)
    {
        col.GetComponent<Gravitable>().Gravities.Remove(this);
    }

}