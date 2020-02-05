using UnityEngine;

/// <summary>Disables grabbing when hand in nograb zone</summary>
public class GrabManager : MonoBehaviour
{
    public OVRGrabber grabber;
    public LayerMask noGrabLayers;

    bool m_isInGrabZone = false;

    [SerializeField] LayerMask m_default = 0;
    [SerializeField] LayerMask m_ignoreCollision = 0;

    Collider[] m_colliders = null;

    void Start()
    {
        m_colliders = GetComponentsInChildren<Collider>(true);
    }

    void FixedUpdate()
    {
        bool iigz = IsInNoGrabZone();
        if (iigz != m_isInGrabZone)
        {
            m_isInGrabZone = iigz;

            SetLayer(m_isInGrabZone ? m_ignoreCollision : m_default);
        }       
    }

    bool IsInNoGrabZone()
    {
        return Physics.OverlapSphere(transform.position, 0.05f, noGrabLayers).Length > 0;
    }
    void SetLayer(LayerMask layer)
    {
        for (int i = 0; i < m_colliders.Length; i++)
        {
            m_colliders[i].gameObject.layer = (int)Mathf.Log(layer.value, 2);
        }
    }
}