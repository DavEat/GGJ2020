using System.Collections;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    public static SphereManager inst;
    void Awake() { inst = this; }

    [Header("Sphere")]
    [SerializeField] DraggableSnap startSnap = null;

    public void ReplaceSphere(Transform sphere, bool dontSnap, bool grab = false)
    {
        if (sphere)
        {
            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            //rb.isKinematic = true;
            Collider col = sphere.GetComponent<Collider>();
            col.enabled = false;

            StartCoroutine(DisapperAppear(sphere, dontSnap, sphere.localScale.x, rb, col));
        }
    }
    IEnumerator DisapperAppear(Transform toFade, bool dontSnap, float startScale, Rigidbody rb, Collider col)
    {
        yield return Fade.fadeScale(toFade, startScale, 0, .3f);

        if (dontSnap)
        {
            toFade.position = startSnap.transform.position;
        }
        else
        {
            OVRGrabbableEvent e = toFade.GetComponent<OVRGrabbableEvent>();
            if (e)
            {
                if (e.grabbedBy)
                    e.grabbedBy.ForceRelease(e);
                startSnap.ForceSnap(e);
            }
        }
        col.enabled = true;
        //rb.isKinematic = false;

        yield return Fade.fadeScale(toFade, 0, startScale, .4f);

    }
}
