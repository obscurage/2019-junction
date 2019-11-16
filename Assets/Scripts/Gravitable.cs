using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravitable : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool applyGravitalForce = false;
    public List<GravitySource> Gravities { get; set; } = new List<GravitySource>();
    public bool ApplyGravitalForce { get => applyGravitalForce; set => applyGravitalForce = value; }

    void OnValidate()
    {
        if (rb is null) { rb = GetComponent<Rigidbody>(); }
        rb.useGravity = false;
        rb.isKinematic = false;
    }

    void FixedUpdate()
    {
        if (!ApplyGravitalForce) { return; }
        rb.velocity = GetAffectedVelocity();
    }

    private Vector3 GetAffectedVelocity()
    {
        if (Gravities.Count == 0) { return rb.velocity; }
        Vector3 result = Vector3.zero;

        foreach (GravitySource gs in Gravities)
        {
            result += gs.GetGravitalForce(transform);
        }

        result *= Time.fixedDeltaTime;
        result += rb.velocity;
        return result;
    }

    public void AddForce(Vector3 force)
    {
        rb.velocity += force;
    }
    public void SetForce(Vector3 force)
    {
        rb.velocity = force;
    }
    public void Freeze()
    {
        SetForce(Vector3.zero);
        rb.angularVelocity = Vector3.zero;
    }
}