using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectToGravity : MonoBehaviour
{
    public bool useGravity = true;

    Rigidbody m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        ApplyGravity();
    }
    void ApplyGravity()
    {
        if (!useGravity) return;

        m_rb.AddForce(Gravity.orientation);
    }
}
