using UnityEngine;

//Disables grabbing when hand in nograb zone
public class GrabManager : MonoBehaviour
{
    public OVRGrabber grabber;
    public LayerMask noGrabLayers;

    private bool _canGrab;

    void Update() {
        if (_canGrab == IsInNoGrabZone()) {
            _canGrab = !IsInNoGrabZone();
        }
        grabber.enabled = _canGrab;

    }

    private bool IsInNoGrabZone() {
        return Physics.OverlapSphere(transform.position, 0.05f, noGrabLayers).Length > 0;
    }
}
