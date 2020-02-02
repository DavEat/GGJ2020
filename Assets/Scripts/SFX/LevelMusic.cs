using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMusic : MonoBehaviour
{
    [SerializeField] AudioClip[] levelMusics = null;

    [SerializeField] int offsetBuildIndex = 2;
    [SerializeField] float fadeInTime = 2f;
    [SerializeField] float fadeOutTime = 1.5f;

    void Start()
    {
        int index = SceneManager.GetActiveScene().buildIndex - offsetBuildIndex;

        for (int i = 0; i < index; i++)
        {
            AudioSource s = gameObject.AddComponent<AudioSource>();
            s.volume = 0;
            s.clip = levelMusics[i];
            s.Play();

            Fade.inst.FadeSound(s, 0, 1, fadeInTime);
        }
    }
    public void FadeMusic()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        for (int i = 0; i < audioSources.Length; i++)
            Fade.inst.FadeSound(audioSources[i], 1, 0, fadeOutTime);
    }
}
