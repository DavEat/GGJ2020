using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbleEventSFX : MonoBehaviour
{
    [SerializeField] AudioClip m_grabSound = null;
    [SerializeField] AudioClip m_hitSound = null;

    [SerializeField] AudioSource m_source = null;
    [SerializeField] AudioSource m_rollSource = null;

    Rigidbody m_rb = null;
    OVRGrabbableEvent m_grabbableEvent = null;

    [SerializeField] float minVelocityToRing = .5f;
    float m_speed;

    void Start()
    {
        m_grabbableEvent = GetComponent<OVRGrabbableEvent>();
        m_grabbableEvent.grabBegin += GrabSound;

        m_rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        /*if (!m_grabbableEvent.isGrabbed && m_rb.velocity.sqrMagnitude < minVelocityToRing)
        {
            if (!m_rollSource.isPlaying)
                m_rollSource.Play();
        }
        else
        {
            if (m_rollSource.isPlaying)
                m_rollSource.Stop();
        }*/

        m_speed = m_rb.velocity.magnitude / minVelocityToRing;
    }

    public void GrabSound(OVRGrabbableEvent grabbable)
    {
        m_source.PlayOneShot(m_grabSound);
        m_rollSource.Pause();
    }

    void OnCollisionEnter(Collision collision)
    {
        m_source.PlayOneShot(m_hitSound);
    }

    void OnCollisionStay(Collision collision)
    {
        if (!m_grabbableEvent.isGrabbed && !m_rollSource.isPlaying && m_speed >= 0.1f /*&& collision.gameObject.tag == "Ground"*/)
        {
            m_rollSource.Play();
        }
        else if (!m_grabbableEvent.isGrabbed && m_rollSource.isPlaying && m_speed < 0.1f /*&& collision.gameObject.tag == "Ground"*/)
        {
            m_rollSource.Pause();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (m_rollSource.isPlaying /*&& collision.gameObject.tag == "Ground"*/)
        {
            m_rollSource.Pause();
        }
    }
}
