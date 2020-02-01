using UnityEngine;

public class SphereManager : MonoBehaviour
{
    public static SphereManager inst;
    void Awake() { inst = this; }

    [Header("Sphere")]
    [SerializeField] DraggableSnap startSnap;

    public void ReplaceSphere(Transform sphere, bool dontSnap, bool grab = false)
    {
        if (sphere)
        {
            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            if (dontSnap)
            {
                sphere.position = startSnap.transform.position;
            }
            else
            {
                OVRGrabbableEvent e = sphere.GetComponent<OVRGrabbableEvent>();
                if (e)
                {
                    if (e.grabbedBy)
                        e.grabbedBy.ForceRelease(e);
                    startSnap.ForceSnap(e);
                }
            }
        }
    }
}
