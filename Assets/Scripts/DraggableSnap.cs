using UnityEngine;
using UnityEngine.Events;

public class DraggableSnap : MonoBehaviour
{
    [SerializeField] Transform m_snapPoint = null;
    [SerializeField] GameObject m_mesh  = null;
    [SerializeField] InteractableTag.Tag m_snapTag;

    OVRGrabbableEvent m_snappedObject = null;

    int m_numberOfObjIn = 0;

    [SerializeField] UnityEvent onSnap = null; 
    [SerializeField] UnityEvent onUnsnap = null;

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

    public void ForceSnap(OVRGrabbableEvent obj)
    {
        m_numberOfObjIn++;
        SnapObject(obj);
    }
    public void SnapObject(OVRGrabbableEvent obj)
    {
        if (m_snappedObject != null)
            return;

        m_snappedObject = obj;

        obj.GetComponent<Rigidbody>().isKinematic = true;

        if (onSnap != null)
            onSnap.Invoke();

        obj.transform.position = m_snapPoint.position;
        obj.transform.rotation = m_snapPoint.rotation;

        if (m_mesh)
            m_mesh.SetActive(false);

        if (obj.grabEnd != null)
            obj.grabEnd -= SnapObject;
        obj.grabBegin += ReleaseObject;
    }

    public void ReleaseObject(OVRGrabbableEvent obj)
    {
        m_snappedObject = null;

        if (m_mesh)
            m_mesh.SetActive(true);

        if (onUnsnap != null)
            onUnsnap.Invoke();

        obj.grabEnd += SnapObject;
        if (obj.grabBegin != null)
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
