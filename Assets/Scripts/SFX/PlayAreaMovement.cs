using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaMovement : MonoBehaviour
{
    [SerializeField] float m_angularSpeedToActive = 1f;
    [SerializeField] float m_angularSpeedToUnactive = 1f;

    public float lastAngle = 0;

    public float delay = 0.2f;
    float m_time = 0;

    public bool startMoving;
    public bool endMoving;

    void FixedUpdate()
    {
        float angle = Vector3.Angle(transform.up, Vector3.up);
        if (Mathf.Abs(lastAngle - angle) > m_angularSpeedToActive)
        {
            lastAngle = angle;

            if (!startMoving)
            {
                startMoving = true;
                endMoving = false;
                StartMoving();
            }
        }
        //else if (m_time < Time.time)
        //    SoundManager.inst.EndRollingRock();
        else if (m_time < Time.time)
        {
            if (!endMoving)
            {
                endMoving = true;
                startMoving = false;
                SoundManager.inst.EndRollingRock();
            }
        }
    }

    void StartMoving()
    {
        SoundManager.inst.StartRollingRock();
        m_time = Time.time + delay;
    }
}
