using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SphereKiller"))
            SphereManager.inst.ReplaceSphere(transform);
    }
}
