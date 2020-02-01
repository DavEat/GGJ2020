using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollision : MonoBehaviour
{
    void Start()
    {
        SphereManager.inst.ReplaceSphere(transform);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SphereKiller"))
            SphereManager.inst.ReplaceSphere(transform);
    }
}
