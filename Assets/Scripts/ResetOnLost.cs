using UnityEngine;

public class ResetOnLost : MonoBehaviour
{
    Transform m_transform = null;
    Transform m_startPosition = null;

    void Start()
    {
        m_transform = GetComponent<Transform>();
        m_startPosition = new GameObject(name + "resetPoint").transform;
        m_startPosition.position = m_transform.position;
        m_startPosition.parent = m_transform.parent;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Outside"))
        {
            Rigidbody rb = m_transform.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            m_transform.position = m_startPosition.position;        }
    }
}
