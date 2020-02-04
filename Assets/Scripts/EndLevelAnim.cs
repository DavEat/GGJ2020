using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class EndLevelAnim : MonoBehaviour
{
    [SerializeField] AudioSource m_endMusic = null;

    [SerializeField] UnityEvent m_startAnim = null;
    [SerializeField] UnityEvent m_endFade = null;

    public void StartFade()
    {
        QuickFadeEndMusic();
        FindObjectOfType<CameraFaderGroup>().FadeOutToWhiteToBlack(m_endFade);
    }
    public void StartLevelCompleteAnim()
    {
        m_startAnim.Invoke();
        SoundManager.inst.FadeOutNatureNoise();
        SoundManager.inst.music.FadeMusic();
    }
    public void QuickFadeEndMusic()
    {
        Fade.inst.FadeSound(m_endMusic, m_endMusic.volume, 0, .2f, .75f);
    }
}