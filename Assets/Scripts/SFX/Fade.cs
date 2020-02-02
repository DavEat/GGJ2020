using UnityEngine;
using System.Collections;

public class Fade : Singleton<Fade>
{
    public void FadeSound(AudioSource sourceToFade, float startVolume, float endVolume, float duration)
    {
        StartCoroutine(fadeSource(sourceToFade, startVolume, endVolume, duration));
    }
    public void CrossFadeSound(AudioSource toUp, AudioSource toDown, float startVolume, float endVolume, float duration)
    {
        StartCoroutine(fadeSource(toDown, endVolume, startVolume, duration, true));
        StartCoroutine(fadeSource(toUp, startVolume, endVolume, duration));
    }

    static IEnumerator fadeSource(AudioSource sourceToFade, float startVolume, float endVolume, float duration, bool stopAtEnd = false)
    {
        float startTime = Time.time;

        while (true)
        {
            float elapsed = Time.time - startTime;

            sourceToFade.volume = Mathf.Clamp01(Mathf.Lerp(startVolume, endVolume, elapsed / duration));

            if (sourceToFade.volume == endVolume)
            {
                if (stopAtEnd) sourceToFade.Stop();

                break;
            }

            yield return null;
        }//end while
    }
}