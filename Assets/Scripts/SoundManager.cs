using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager inst;
    void Awake() { inst = this; }

    [SerializeField] AudioSource m_natureNoise = null;

    //[SerializeField] AudioClip m_rollingRockClip = null;
    //[SerializeField] AudioClip m_rollingRockTailClip = null;
    [SerializeField] AudioSource m_rollingRockSource = null;
    [SerializeField] AudioSource m_rollingRockTailSource = null;

    public LevelMusic music = null;
    [SerializeField] float m_natureNoiseVolume = .8f;
    
    public void StartRollingRock()
    {
        //if (m_rollingRockSource.isPlaying) return;

        m_rollingRockSource.Play();

        if (m_rollingRockTailSource.isPlaying)
            Fade.inst.CrossFadeSound(m_rollingRockSource, m_rollingRockTailSource, 0, 1, .2f);

    }
    public void EndRollingRock()
    {
        //if (!m_rollingRockSource.isPlaying || m_rollingRockTailSource.isPlaying)
        //    return;

        m_rollingRockTailSource.volume = 0;
        m_rollingRockTailSource.Play();

        Fade.inst.CrossFadeSound(m_rollingRockTailSource, m_rollingRockSource, 0, 1, 1.2f);
    }

    public void FadeInNatureNoise()
    {
        Fade.inst.FadeSound(m_natureNoise, 0, m_natureNoiseVolume, music.fadeInTime);
    }
    public void FadeOutNatureNoise()
    {
        Fade.inst.FadeSound(m_natureNoise, m_natureNoise.volume, 0, music.fadeOutTime);
    }
}
