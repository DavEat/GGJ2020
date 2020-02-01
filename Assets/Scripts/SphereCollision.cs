using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollision : MonoBehaviour
{
    [SerializeField] bool m_dontSnap = false;

    void Start()
    {
        if (!m_dontSnap)
            SphereManager.inst.ReplaceSphere(transform, m_dontSnap);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SphereKiller"))
        {
            SphereManager.inst.ReplaceSphere(transform, m_dontSnap);
        }
    }
}