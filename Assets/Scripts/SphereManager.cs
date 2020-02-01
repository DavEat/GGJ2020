using UnityEngine;

public class SphereManager : MonoBehaviour
{
    public static SphereManager inst;
    void Awake() { inst = this; }

    [Header("Sphere")]
    [SerializeField] DraggableSnap startSnap;

    public void ReplaceSphere(Transform sphere, bool dontSnap)
    {
        if (sphere)
        {
            if (dontSnap)
            {
                sphere.position = startSnap.transform.position;
            }
            else
            {
                OVRGrabbableEvent e = sphere.GetComponent<OVRGrabbableEvent>();
                if (e)
                    startSnap.ForceSnap(e);
            }
        }
    }
}
