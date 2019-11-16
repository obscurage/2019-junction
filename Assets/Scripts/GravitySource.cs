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
        return new Vector3
        (transform.position.x - target.position.x
        , transform.position.y - target.position.y
        , transform.position.z - target.position.z)
        * (gravitalForce / Vector3.Distance(transform.position, target.position));
        ;
    }

    void OnValidate()
    {
        gameObject.name = "GravitalField";
        gameObject.layer = 10; // 10 = gravitalfield
        if(collider is null) { collider = GetComponent<SphereCollider>(); }
        collider.isTrigger = true;
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