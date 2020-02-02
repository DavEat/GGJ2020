using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class EndLevelAnim : MonoBehaviour
{
    /*[SerializeField] Light m_pointLight = null;
    [SerializeField] DraggableSnap draggable = null;

    [SerializeField] bool m_play = false;

    [SerializeField] float m_lightDelay = .2f, lightTime = 2f, lightTargetIntencity = 2f, lightTargetRange = 2f;

    float lightStartIntencity = 0, lightStartRange = 0;

    void Update()
    {
        if (m_play)
        {
            m_play = false;
            StartAnim();
        }
    }
    public void StartAnim()
    {
        lightStartIntencity = m_pointLight.intensity;
        lightStartRange = m_pointLight.range;

        StartCoroutine(Animation());
    }
    IEnumerator Animation()
    {
        for (float i = 0; i < 1f; i += Time.deltaTime / (m_lightDelay + lightTime))
        {
            m_pointLight.range = Mathf.Lerp(lightStartRange, lightTargetRange, i);
            m_pointLight.intensity = Mathf.Lerp(lightStartIntencity, lightTargetIntencity, i);
            yield return null;
        }
    }*/

    [SerializeField] UnityEvent m_endFade;
    [SerializeField] UnityEvent m_end;

    public void StartFade()
    {
        FindObjectOfType<CameraFaderGroup>().FadeOutToWhiteToBlack(m_endFade);
    }
    public void End()
    {
        m_end.Invoke();
        SoundManager.inst.music.FadeMusic();
    }
}