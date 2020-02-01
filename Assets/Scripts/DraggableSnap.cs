﻿using UnityEngine;

public class DraggableSnap : MonoBehaviour
{
    [SerializeField] Transform m_snapPoint;
    [SerializeField] GameObject m_mesh;
    [SerializeField] InteractableTag.Tag m_snapTag;

    OVRGrabbableEvent m_snappedObject;

    int m_numberOfObjIn = 0;

    void OnTriggerEnter(Collider other)
    {
        if (m_snappedObject != null)
            return;

        OVRGrabbableEvent grabbable = other.GetComponent<OVRGrabbableEvent>();
        if (grabbable != null && grabbable.grabbedBy != null)
        {
            if (m_snapTag != InteractableTag.Tag.none)
            {
                InteractableTag itag = other.GetComponent<InteractableTag>();
                if (itag.tag == m_snapTag)
                    CandidateToSnapEnter(grabbable);
            } else CandidateToSnapEnter(grabbable);
        }
    }

    void CandidateToSnapEnter(OVRGrabbableEvent grabbable)
    {
        m_numberOfObjIn++;
        grabbable.grabEnd += SnapObject;
        if (m_mesh)
            m_mesh.SetActive(true);
    }
    void CandidateToSnapExit(OVRGrabbableEvent grabbable)
    {
        grabbable.grabEnd -= SnapObject;
        m_numberOfObjIn--;
        if (m_numberOfObjIn < 0)
            m_numberOfObjIn = 0;

        if (m_numberOfObjIn == 0 && m_mesh)
            m_mesh.SetActive(false);
    }

    void SnapObject(OVRGrabbableEvent obj)
    {
        if (m_snappedObject != null)
            return;

        m_snappedObject = obj;

        obj.GetComponent<Rigidbody>().isKinematic = true;


        obj.transform.position = m_snapPoint.position;
        obj.transform.rotation = m_snapPoint.rotation;

        if (m_mesh)
            m_mesh.SetActive(false);

        obj.grabEnd -= SnapObject;
        obj.grabBegin += ReleaseObject;
    }

    void ReleaseObject(OVRGrabbableEvent obj)
    {
        m_snappedObject = null;

        if (m_mesh)
            m_mesh.SetActive(true);

        obj.grabEnd += SnapObject;
        obj.grabBegin -= ReleaseObject;
    }

    void OnTriggerExit(Collider other)
    {
        OVRGrabbableEvent grabbable = other.GetComponent<OVRGrabbableEvent>();
        if (grabbable != null && grabbable.grabbedBy != null)
        {
            if (m_snapTag != InteractableTag.Tag.none)
            {
                InteractableTag itag = other.GetComponent<InteractableTag>();
                if (itag.tag == m_snapTag)
                    CandidateToSnapExit(grabbable);
            } else CandidateToSnapExit(grabbable);
        }
    }
}
