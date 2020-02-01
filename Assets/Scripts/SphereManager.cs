using UnityEngine;

public class SphereManager : Singleton<SphereManager>
{
    [Header("Sphere")]
    [SerializeField] Transform startPosition;

    public void ReplaceSphere(Transform sphere)
    {
        if (startPosition)
        {
            sphere.position = startPosition.position;
            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
