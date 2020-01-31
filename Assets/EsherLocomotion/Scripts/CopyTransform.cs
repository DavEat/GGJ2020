using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform target;
    public bool localValues;
    public bool copyPosition;
    public bool copyRotation;
    public bool copyScale;

    void Update() {
        if (localValues) {
            if (copyPosition)
                transform.localPosition = target.position;
            if (copyRotation)
                transform.localRotation = target.rotation;
        } else {
            if (copyPosition)
                transform.position = target.position;
            if (copyRotation)
                transform.rotation = target.rotation;
        }

        if (copyScale) {
            transform.localScale = target.lossyScale;
        }
    }
}