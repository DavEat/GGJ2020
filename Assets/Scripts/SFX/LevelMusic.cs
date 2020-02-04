using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LevelMusic : MonoBehaviour
{
    [SerializeField] AudioMixerGroup m_mixer = null;
    [SerializeField] AudioClip[] m_levelMusics = null;

    [SerializeField] int m_offsetBuildIndex = 2;
    [SerializeField] public float fadeInTime = 7f;
    [SerializeField] public float fadeOutTime = 2.5f;

    void Start()
    {
        int index = SceneManager.GetActiveScene().buildIndex - m_offsetBuildIndex;
        if (index > m_levelMusics.Length)
            index = m_levelMusics.Length;

        for (int i = 0; i < index; i++)
        {
            AudioSource s = gameObject.AddComponent<AudioSource>();
            s.volume = 0;
            s.loop = true;
            s.outputAudioMixerGroup = m_mixer;
            s.clip = m_levelMusics[i];
            s.Play();

            Fade.inst.FadeSound(s, s.volume, 1, fadeInTime);
        }
    }
    public void FadeMusic()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        for (int i = 0; i < audioSources.Length; i++)
            Fade.inst.FadeSound(audioSources[i], audioSources[i].volume, 0, fadeOutTime);
    }
}