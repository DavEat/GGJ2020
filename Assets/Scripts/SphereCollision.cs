using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollision : MonoBehaviour
{
    [SerializeField] bool m_snapAtStart = true;

    void Start()
    {
        if (m_snapAtStart)
            SphereManager.inst.ReplaceSphere(transform);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SphereKiller"))
            SphereManager.inst.ReplaceSphere(transform);
    }
}