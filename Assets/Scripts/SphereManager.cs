using UnityEngine;

public class SphereManager : Singleton<SphereManager>
{
    [Header("Sphere")]
    [SerializeField] DraggableSnap startSnap;

    public void ReplaceSphere(Transform sphere)
    {
        if (sphere)
        {
            OVRGrabbableEvent e = sphere.GetComponent<OVRGrabbableEvent>();
            if (e)
                startSnap.ForceSnap(e);
        }
    }
}
