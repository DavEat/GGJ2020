using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRGrabbableEvent : OVRGrabbable
{
    public delegate void GrabEvent(OVRGrabbableEvent grabbable);
    public GrabEvent grabBegin;
    public GrabEvent grabEnd;

    [SerializeField] bool m_parentToGrabber = true;

    override public void GrabBegin(OVRGrabber hand, Collider grabPoint) {
        base.GrabBegin(hand, grabPoint);
        if (grabBegin != null)
            grabBegin.Invoke(this);

        if (m_parentToGrabber)
            transform.parent = hand.transform;
    }

    /// <summary>
    /// Notifies the object that it has been released.
    /// </summary>
    override public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        transform.parent = null;

        base.GrabEnd(linearVelocity, angularVelocity);
        if (grabEnd != null)
            grabEnd.Invoke(this);
    }
}
