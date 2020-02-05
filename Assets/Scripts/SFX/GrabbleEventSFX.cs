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

    [SerializeField] float m_rollingMaxSpeed = 1;
    [SerializeField] AnimationCurve m_volumeCurve = null;
    [SerializeField] AnimationCurve m_pitchCurve = null;

    void Start()
    {
        m_grabbableEvent = GetComponent<OVRGrabbableEvent>();
        m_grabbableEvent.grabBegin += GrabSound;

        m_rb = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (m_rb.velocity.magnitude > .1f)
            m_source.PlayOneShot(m_hitSound);
    }
    void OnCollisionStay(Collision collision)
    {
        if (m_grabbableEvent.isGrabbed)
            return;

        var speed = m_rb.velocity.magnitude;
        Debug.Log("speed= " + speed);

        // normalize speed into 0-1
        var scaledVelocity = Remap(Mathf.Clamp(speed, 0, m_rollingMaxSpeed), 0, m_rollingMaxSpeed, 0, 1);

        // set volume based on volume curve
        m_rollSource.volume = m_volumeCurve.Evaluate(scaledVelocity);

        // set pitch based on pitch curve
        m_rollSource.pitch = m_pitchCurve.Evaluate(scaledVelocity);

        if (!m_rollSource.isPlaying)
            m_rollSource.Play();
    }
    void OnCollisionExit(Collision collision)
    {
        if (m_rollSource.isPlaying)
        {
            m_rollSource.Pause();
        }
    }
    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    public void GrabSound(OVRGrabbableEvent grabbable)
    {
        m_source.PlayOneShot(m_grabSound);
        m_rollSource.Pause();
    }
}
